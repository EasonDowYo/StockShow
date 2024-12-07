class EasonTable {

    CreateTable(data, target) {
        let table = document.getElementById(target);
        table.style.border = "3px solid red";  // Table style
        let columnNames = Object.keys(data[0]);
        // Create Rows
        for (let i = 0; i < data.length; i++) {// i is Row number
            let row = table.insertRow(i);
            for (let j = 0; j < columnNames.length; j++) {
                let cell = row.insertCell(j);// j is Column number
                cell.innerHTML = data[i][columnNames[j]];
            }
        }

        // Create Header
        let header = table.createTHead();
        let header_row = header.insertRow(0);
        for (let i = 0; i < columnNames.length; i++) {
            let header_cell = header_row.insertCell(i);
            header_cell.innerHTML = columnNames[i];
        }

        let td_el = document.querySelectorAll('td');
        for (let ii = 0; ii < td_el.length; ii++) {
            td_el[ii].style.border = "1px solid green";  // Cell style
        }
    }

    BuildTableWithTwoButton(data, target) {
        console.log(data)
        var table = document.getElementById(target);
        table.style.border = "3px solid red";
        var columnNames = Object.keys(data[0]);

        // Create Rows
        for (let i = 0; i < data.length; i++) {// i is Row number
            let row = table.insertRow(i);
            for (let j = 0; j < columnNames.length + 1; j++) {

                if (j == 0) {
                    let cell = row.insertCell();
                    let btn = document.createElement('button');
                    btn.type = "button";
                    btn.className = "m-2 btn btn-primary";
                    btn.textContent = "Edit";
                    btn.onclick = function () {
                        // 获取当前按钮所在的行
                        var srow = this.parentNode.parentNode;
                        document.getElementById('StockNo_edit').value = srow.cells[1].textContent;
                        document.getElementById('StockName_edit').value = srow.cells[2].textContent;
                        document.getElementById('StockNoName_edit').value = srow.cells[3].textContent;
                        document.getElementById('StockType_edit').value = srow.cells[4].textContent;
                        document.getElementById('StockTypeIndex_edit').selectedIndex = srow.cells[5].textContent-1;
                        document.getElementById('Note1_edit').value = srow.cells[6].textContent;
                        document.getElementById('Note2_edit').value = srow.cells[7].textContent;
                        document.getElementById('Note3_edit').value = srow.cells[8].textContent;
                        document.getElementById('Note4_edit').value = srow.cells[9].textContent;
                        document.getElementById('Note5_edit').value = srow.cells[10].textContent;
                        document.getElementById('Note6_edit').value = srow.cells[11].textContent;
                        document.getElementById('Note7_edit').value = srow.cells[12].textContent;
                        //postObj.StockName = document.getElementById('StockName_edit').value;
                        //postObj.StockNoName = document.getElementById('StockNoName_edit').value;
                        //postObj.StockTypeIndex = document.getElementById('StockTypeIndex_edit').value;
                        //postObj.StockType = document.getElementById('StockType_edit').value;
                        //postObj.Note1 = document.getElementById('Note1_edit').value;
                        //postObj.Note2 = document.getElementById('Note2_edit').value;
                        //postObj.Note3 = document.getElementById('Note3_edit').value;
                        //postObj.Note4 = document.getElementById('Note4_edit').value;
                        //postObj.Note5 = document.getElementById('Note5_edit').value;
                        //postObj.Note6 = document.getElementById('Note6_edit').value;
                        //postObj.Note7 = document.getElementById('Note7_edit').value;
                        // 获取当前行的数据
                        let obj = {}
                        obj.StockNo_edit= srow.cells[1].textContent;
                        obj.StockName_edit = srow.cells[2].textContent;
                        obj.StockNoName_edit = srow.cells[3].textContent;
                        obj.StockType_edit = srow.cells[4].textContent;
                        obj.StockTypeIndex_edit = srow.cells[5].textContent;
                        obj.Note1_edit = srow.cells[6].textContent;
                        obj.Note2_edit = srow.cells[7].textContent;
                        obj.Note3_edit = srow.cells[8].textContent;
                        obj.Note4_edit = srow.cells[9].textContent;
                        obj.Note5_edit = srow.cells[10].textContent;
                        obj.Note6_edit = srow.cells[11].textContent;
                        obj.Note7_edit = srow.cells[12].textContent;
                        // 在这里你可以对获取的数据进行任何操作
                        console.log(obj);
                    };
                    btn.setAttribute('data-bs-toggle', 'modal');
                    btn.setAttribute('data-bs-target', '#exampleModal');
                    cell.appendChild(btn);

                    let btn2 = document.createElement('input');
                    btn2.type = "button";
                    btn2.className = "m-2 btn btn-primary";
                    btn2.value = "Delete";
                    btn2.onclick = function () { console.log("Delete"); };
                    cell.appendChild(btn2);

                } else {
                    let cell = row.insertCell(j);// j is Column number
                    cell.innerHTML = data[i][columnNames[j-1]];
                }


            }
        }

        // Create Header
        var header = table.createTHead();
        var header_row = header.insertRow(0);
        for (let i = 0; i < columnNames.length + 1; i++) {
            if (i == 0) {
                let header_cell = header_row.insertCell(i);
                header_cell.innerHTML = "Button";
            } else {
                let header_cell = header_row.insertCell(i);
                header_cell.innerHTML = columnNames[i-1];

            }
        }

        var td_el = document.querySelectorAll('td');
        for (var ii = 0; ii < td_el.length; ii++) {
            td_el[ii].style.padding = "3px";
            td_el[ii].style.border = "1px solid gray";

        }
    }



}




function EditFunc(oo) {
    console.log("EditFunc" + oo);
}