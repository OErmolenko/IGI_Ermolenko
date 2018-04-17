var d = document;

var mark, capasity, carrying;

function AddRow() {
    mark = d.getElementById('mark').nodeValue;
    capasity = d.getElementById('capasity').nodeValue;
    carrying = d.getElementById('carrying').nodeValue;

    var tbody = d.getElementsByClassName('table').getElementByTagName('TBODY')[0];

    var row = d.createElement('TR');
    tbody.appendChild(row);

    var td1 = d.createElement('TD');
    var td2 = d.createElement('TD');
    var td3 = d.createElement('TD');

    row.appendChild(td1);
    row.appendChild(td2);
    row.appendChild(td3);

    td1.innerHTML = mark;
    td2.innerHTML = capasity;
    td3.innerHTML = carrying;
}