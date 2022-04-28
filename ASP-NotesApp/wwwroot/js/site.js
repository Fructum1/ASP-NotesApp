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
        $("#notificationMessage").text("Успешно удалено").show();
    });
}

const archive = function (noteId) {
    $.ajax({
        url: "/Note/Archive?id=" + noteId,
        type: 'POST',
    }).done(function (e) {
        var notif = $("#notificationContainer");
        notif.addClass("active");
        $("#notificationMessage").text("Успешно архивировано!");
        setTimeout(function () {
            notif.removeClass("active");
        }, 2000)
    
        $.ajax({
            url: "/Note/GetNotesList",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#123").html(data);
                alert(data);
            }
        })        
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
        $("#notificationMessage").text("Успешно удалено").show();
    });
}

const clickOnCreate = function () {
    var innerArrow = document.getElementById("img");
    innerArrow.setAttribute("transform", "rotate(45)");
}
