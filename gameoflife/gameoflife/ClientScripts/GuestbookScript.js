function addComment(author, text) {
    document.getElementById("comments").innerHTML += "<tr><td class='author uicontrol'>" +
    author + "</td><td class='text uicontrol'>" + text + "</td></tr>";
}