////const Edit = function (noteId) {

////    let url = "/Note/Edit?id=" + noteId;

////    $("#noteModalBodyDiv").load(url, function () {
////        $("#noteModal").modal("show");

////    })
////}

const putInCan = function (noteId) {
    $.ajax({
        url: "/Note/Delete?id=" + noteId,
        type: 'POST',

    }).done(function () {
        alert("Added");
    });
}