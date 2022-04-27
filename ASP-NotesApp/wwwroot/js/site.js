const Edit = function (noteId) {

    let url = "/Note/Edit?id=" + noteId;

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");

    })
}

const Create = function () {

    let url = "/Note/Create";

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");

    })
}

const putInCan = function (noteId) {
    $.ajax({
        url: "/Note/Delete?id=" + noteId,
        type: 'POST',

    }).done(function () {
        document.location.reload();
    });
}

const archive = function (noteId) {
    $.ajax({
        url: "/Note/Archive?id=" + noteId,
        type: 'POST',
    }).done(function () {
        document.location.reload();
    });
}

const unArchive = function (noteId) {
    $.ajax({
        url: "/Note/UnArchive?id=" + noteId,
        type: 'POST',
    }).done(function () {
        document.location.reload();
    });
}

const deleteFromTrashCan = function (noteId) {
    $.ajax({
        url: "/Note/DeleteFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        document.location.reload();
    });
}

const recoverFromTrashCan = function (noteId) {
    $.ajax({
        url: "/Note/RecoverFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        document.location.reload();
    });
}

const deleteNote = function (noteId) {
    $.ajax({
        url: "/Note/DeleteFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        document.location.reload();
    });
}

const clickOnCreate = function () {
    var innerArrow = document.getElementById("img");
    innerArrow.setAttribute("transform", "rotate(45)");
}