function get(id) {
    return document.getElementById(id);
}

function addRow(author, desc, i) {
    get("patterns_list").innerHTML += "<tr><td class='uicontrol author left'>" +
    author + "</td><td class='uicontrol description center'>" +
    desc + "</td><td class='uicontrol button right load' onclick='load(" + i + ")'> Load</td></tr>";
}

function load(i) {
    get("selectedPatternId").value = i;
    get("form").submit();
}
