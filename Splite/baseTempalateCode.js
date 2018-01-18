/* =================================
* Alexander Borovskikh script for Acrobat
* Split Document - divides the document on formats that are available in the document and stores it close to the original
* Search Format - searches for the first, not the size of the selected page list and select it
* Create for NT
* ==================================
* */

splitDocument = app.trustedFunction(function () {
    var thisDocument = this,
        pages = thisDocument.numPages,
        pathBase = thisDocument.path;

    function createArr(doc, pages) {
        var fArray = [], tempArr2 = [], i = 0;

        while ( i < pages ) {

            var tempArr = doc.getPageBox('Crop', i);

            tempArr2[0] = Math.floor((tempArr[2] - tempArr[0]) / 2.834);
            tempArr2[1] = Math.floor((tempArr[1] - tempArr[3]) / 2.834);

            fArray.push(tempArr2);
            tempArr2 = [];
            i++;
        }

        return fArray;
    }

    function sorting(m) {
        var temp = [ m[0][0], m[0][1] ],
            arr1 = [], arr2 = [], j = 0, k = 0;

        while ( j < m.length ) {
            var w = m[j][0],
                h = m[j][1];
            if (( w === temp[0] && h === temp[1] ) || ( w === temp[1] && h === temp[0] )) {
                arr1[k] = j;
                j++; k++;
                if ( j === m.length ) { arr1[k] = rangeFormat(w,h); arr2.push(arr1); break; }
            } else {
                arr1[k] = rangeFormat(m[j-1][0],m[j-1][1]);
                arr2.push(arr1);
                arr1 = [];
                k = 0;
                temp = [ m[j][0] ,  m[j][1] ];
            }
        }

        return arr2;
    }

    function nameFile(path, num, n) {
        var p = path.split("/"),
            name = p[p.length-1],
            sName = name.split("."),
            fileName, pathToFile;
        p.pop();
        sName.pop();
        sName.push("edit_" + (num + 1) + n + ".pdf");
        fileName = sName.join("_");
        p.push(fileName);
        pathToFile = p.join("/");

        return pathToFile;
    }

    function rangeFormat(w, h) {

        var formats = [
            [ 220 , 140 , 'a5' ] ,
            [ 305 , 205 , 'a4' ] ,
            [ 430 , 285 , 'a3' ] ,
            [ 600 , 410 , 'a2' ] ,
            [ 850 , 585 , 'a1' ] ,
            [ 1195 , 835 , 'a0' ] ,
            [ 640 , 585 , '594x630' ]
        ];

        var format = null,
            result;

        for ($i = 0; $i < formats.length; $i++) {

            if ( ( w <= formats[$i][0] && w >= formats[$i][1] ) && ( h <= formats[$i][0] && h >= formats[$i][1] ) ) {
                format = '_' + formats[$i][2];
                break;
            }
        }

        result = (format != null) ? format : '_un_format';

        return result;

    }

    function choiceFormatName( w , h ) {

        var formats = [ [ 297 , 210 , 'a4' ] ,
                        [ 297 , 420 , 'a3' ] ,
                        [ 148 , 210 , 'a5' ] ,
                        [ 594 , 420 , 'a2' ] ,
                        [ 841 , 594 , 'a1' ] ,
                        [ 1189 , 841 , 'a0' ] ,
                        [ 594 , 630 , '594x630' ] ];

        var format = null,
            result;

        for ($i = 0; $i < formats.length; $i++) {
            if (( w === formats[$i][0] && h === formats[$i][1] ) || ( w === formats[$i][1] && h === formats[$i][0] )) {
                format = '_' + formats[$i][2];
                break;
            }
        }

        result = (format != null) ? format : '_un_format';

        return result;
    }

    function newDocument(path, start, end, num, n) {
        var newDoc = app.newDoc(), name;
        newDoc.insertPages({
            nPage: newDoc.numPages - 1,
            cPath: path,
            nStart: start,
            nEnd: end
        });

        if (newDoc.numPages > 1) {
            newDoc.deletePages(0);
            name = nameFile(path, num, n);
            newDoc.saveAs(name);
            newDoc.closeDoc(true);
        }
    }

    var pagesArray = createArr(thisDocument, pages);
    var sort = sorting(pagesArray);

    for (i = 0; i < sort.length; i++) {
        var s = sort[i][0],
            e = sort[i][sort[i].length-2],
            n = sort[i][sort[i].length-1];
        newDocument(pathBase, s, e, i, n);
    }

});