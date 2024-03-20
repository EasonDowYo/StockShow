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
}