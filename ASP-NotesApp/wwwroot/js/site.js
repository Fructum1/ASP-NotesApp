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

const createNote = function () {
    var data = $("#create-note-form").serialize();
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/Note/Create',
        data: data,
        success: function (result) {
            var notif = $("#notificationContainer");
            notif.addClass("active");
            $("#notificationMessage").text("Успешно создано!");
            setTimeout(function () {
                notif.removeClass("active");
            }, 1000)

            $.ajax({
                url: "/Note/GetNotesList",
                type: 'GET',
                cache: false,
                success: function OnSuccess(data) {
                    $("#content-note").html(data);
                    $("#noteModal").modal("hide");
                    resetMenuHighlight();
                    $("#active-note").addClass('active');
                }
            })
        }
    })
}

const editNote = function (status) {
    var data = $("#edit-note-form").serialize();
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/Note/Edit',
        data: data,
        success: function (result) {
            var notif = $("#notificationContainer");
            notif.addClass("active");
            $("#notificationMessage").text("Успешно изменено!");
            setTimeout(function () {
                notif.removeClass("active");
            }, 1000)

            $.ajax({
                url: "/Note/GetNotesList?status=" + status,
                type: 'GET',
                cache: false,
                success: function OnSuccess(data) {
                    $("#content-note").html(data);
                    $("#noteModal").modal("hide");
                
                }
            })
        }
    })
}

const putInCan = function (noteId, status) {
    $.ajax({
        url: "/Note/Delete?id=" + noteId,
        type: 'POST',

    }).done(function (e) {
        var notif = $("#notificationContainer");
        notif.addClass("active");
        $("#notificationMessage").text("Успешно удалено!");
        setTimeout(function () {
            notif.removeClass("active");
        }, 1000)

        $.ajax({
            url: "/Note/GetNotesList?status=" + status,
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
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
        }, 1000)
    
        $.ajax({
            url: "/Note/GetNotesList",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })        
    });
}

const unArchive = function (noteId) {
    $.ajax({
        url: "/Note/UnArchive?id=" + noteId,
        type: 'POST',
    }).done(function () {
        var notif = $("#notificationContainer");
        notif.addClass("active");
        $("#notificationMessage").text("Успешно разархивировано!");
        setTimeout(function () {
            notif.removeClass("active");
        }, 1000)

        $.ajax({
            url: "/Note/GetNotesList?status=1",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
    });
}

const deleteFromTrashCan = function (noteId) {
    $.ajax({
        url: "/Note/DeleteFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        var notif = $("#notificationContainer");
        notif.addClass("active");
        $("#notificationMessage").text("Успешно удалено!");
        setTimeout(function () {
            notif.removeClass("active");
        }, 1000)

        $.ajax({
            url: "/Note/GetNotesList?status=2",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
    });
}

const recoverFromTrashCan = function (noteId) {
    $.ajax({
        url: "/Note/RecoverFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        var notif = $("#notificationContainer");
        notif.addClass("active");
        $("#notificationMessage").text("Успешно восстановлено!");
        setTimeout(function () {
            notif.removeClass("active");
        }, 1000)

        $.ajax({
            url: "/Note/GetNotesList?status=2",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
    });
}

const deleteNote = function (noteId, status) {
    $.ajax({
        url: "/Note/DeleteFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        var notif = $("#notificationContainer");
        notif.addClass("active");
        $("#notificationMessage").text("Успешно удалено!");
        setTimeout(function () {
            notif.removeClass("active");
        }, 1000)

        $.ajax({
            url: "/Note/GetNotesList?status=" + status,
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
    });
}

const clickOnCreate = function () {
    var innerArrow = document.getElementById("img");
    innerArrow.setAttribute("transform", "rotate(45)");
}

const search = function (status) {
    var attribute = document.getElementById("input-search").value;
    let url = "/Note/GetNotesList?attribute=" + attribute + '&status=' + status;

    if (status == 0) {
        resetMenuHighlight();
        $("#active-note").addClass('active');
    }

    $("#content-note").load(url, function () {

        })
}

const pin = function (noteId) {
    $.ajax({
        url: "/Note/PinNote?id=" + noteId,
        type: 'POST',

    }).done(function (e) {
        $.ajax({
            url: "/Note/GetNotesList",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
    });
}

const unPin = function (noteId) {
    $.ajax({
        url: "/Note/UnPinNote?id=" + noteId,
        type: 'POST',

    }).done(function (e) {
        $.ajax({
            url: "/Note/GetNotesList",
            type: 'GET',
            cache: false,
            success: function OnSuccess(data) {
                $("#content-note").html(data);
            }
        })
    });
}

const resetMenuHighlight = function () {
    $(".nav-link.py-3.border-bottom").each(function () {
        $(this).removeClass('active');
    })
}
