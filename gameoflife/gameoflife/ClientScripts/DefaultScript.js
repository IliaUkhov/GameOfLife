var width = 15.0;
var height = 15.0;
var turnTime = 0.5;
var ongoing = false;
var step = 0;

function setField() {
    var cellWidth = (screen.height - 180) / width;
    var cellHeight = cellWidth - 4;
    for (i = 0; i < height; ++i) {
        get("field").innerHTML += "<tr>";
        var tds = "";
        for (j = 0; j < width; ++j) {
            var id = i * 100 + j
            tds += "<td onclick='revertCell(" + id + ")' width=" + cellWidth + 
                " height=" + cellHeight + "><div class='cell' id='c" + id + "'></div></td>";
        }
        get("field").innerHTML += tds + "</tr>";
    }
}

function get(id) {
    return document.getElementById(id);
}

function getCell(i) {
    return get("c" + i);
}

var neighbors = [-100, -99, 1, 101, 100, 99, -1, -101];
var alive = new Set();

function start() {
    if (alive.size == 0 || ongoing) return;
    get("start_button").setAttribute("class", "color_button ongoing");
    ongoing = true;
    iterate();
    gameTimer();
}

function pause() {
    get("start_button").setAttribute("class", "color_button");
    ongoing = false;
    clearTimeout(timeout);
}

function clearf() {
    if (ongoing) {
        pause();
    }
    for (c of alive) {
        revertCell(c);
    }
    alive.clear();
    step = 0;
    get("step_div").textContent = "Step: 0";
}

function iterate() {
    step += 1;
    if (alive.size == 0) {
        pause();
        return;
    }
    var cellsThatWillLive = new Set();
    for (c of alive) {
        if (keepAlive(c)) cellsThatWillLive.add(c);
    }
    for (c of new Set([...alive].filter(i => !cellsThatWillLive.has(i)))) {
        getCell(c).setAttribute("class", "cell_whitesmoke");
    }
    var newCells = populateNewCells();
    for (c of new Set([...newCells].filter(i => !alive.has(i)))) {
        getCell(c).setAttribute("class", "cell_red");
    }
    alive = new Set([...cellsThatWillLive, ...newCells]);
    get("step_div").textContent = "Step: " + step;
}

function keepAlive(c) {
    var neighborCount = 0;
    for (n of neighbors) {
        if (isValid(c + n) && alive.has(c + n)) {
            ++neighborCount;
            if (neighborCount > 3) {
                return false;
            }
        }
    }
    return neighborCount > 1;
}

function populateNewCells() {
    neighborCounts = {};
    for (c of alive) {
        for (n of neighbors) {
            if (isValid(c + n) && !alive.has(c + n)) {
                if (neighborCounts.hasOwnProperty(c + n)) {
                    ++neighborCounts[c + n];
                } else {
                    neighborCounts[c + n] = 1;
                }
            }
        }
    }
    var newCells = new Set();
    for (var c in neighborCounts) {
        if (neighborCounts[c] == 3) {
            newCells.add(parseInt(c));
        }
    }
    return newCells;
}

function revertCell(i) {
    if (alive.has(i)) {
        alive.delete(i);
        getCell(i).setAttribute("class", "cell_whitesmoke");
    } else {
        alive.add(i);
        getCell(i).setAttribute("class", "cell_red");
    }
}

function isValid(i) {
    return (i % 100 >= 0 && i % 100 < width) && (i / 100 < height)
}

function gameTimer() {
    timeout = setTimeout(addTurnTime, turnTime * 1000);
}

function addTurnTime() {
    gameTimer();
    iterate();
}

function resize(w, h) {
    clearf();
    get("field").innerHTML = "";
    width = w, height = h;
    setField();
    get("resize_div").textContent = "Size: " + w + "x" + h;
}

function sharePattern() {
    get("aliveCells").value = archivePattern();
    get("patternHeight").value = height;
    get("patternWidth").value = width;
}

function load() {
    get("load_form").submit();
}

function archivePattern() {
    str = "";
    for (cell of alive) {
        str += " ";
        str += cell;
    }
    return str.substr(1);
}

function setSpeed(k) {
    get("speed_div").textContent = 2 * k + " rounds/s";
    turnTime = 0.5 / k;
}

function setPattern(cellsString) {
    var cells = cellsString.split(" ");
    for (cell of cells) {
        revertCell(parseInt(cell));
    }
}
