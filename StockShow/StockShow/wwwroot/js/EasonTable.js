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

    var table = document.getElementById(target);
    table.style.border = "3px solid red";
    var columnNames = Object.keys(data[0]);

    // Create Rows
    for (let i = 0; i < data.length; i++) {// i is Row number
        let row = table.insertRow(i);
        for (let j = 0; j < columnNames.length + 1; j++) {

            if (j == 0) {
                let cell = row.insertCell();
                let btn = document.createElement('input');
                btn.type = "button";
                btn.className = "m-2 btn btn-primary";
                btn.value = "Edit";
                btn.onclick = function () { console.log("Edit"); };
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
                cell.innerHTML = data[i][columnNames[j]];
            }


        }
    }

    // Create Header
    var header = table.createTHead();
    var header_row = header.insertRow(0);
    for (let i = 0; i < columnNames.length; i++) {
        let header_cell = header_row.insertCell(i);
        header_cell.innerHTML = columnNames[i];
    }

    var td_el = document.querySelectorAll('td');
    for (var ii = 0; ii < td_el.length; ii++) {
        td_el[ii].style.padding = "3px";
        td_el[ii].style.border = "1px solid gray";

    }
}



}