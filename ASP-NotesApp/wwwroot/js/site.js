const Edit = function (noteId) {

    var url = "/Note/Edit?id=" + noteId;

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");

    })
}

const Create = function () {

    var url = "/Note/Create";

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");
    })
}

const EditUser = function (userId) {
    var url = "/User/Edit?id=" + userId;
    var data = $("#edit-user-form").serialize();
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: function () {
            var newName = $("#user-name-from-form").val();
            var newSurname = $("#user-surname-from-form").val();
            $("#user-name-edit").text(newName);
            $("#user-surname-edit").text(newSurname);

            var notif = $("#notificationContainer");
            notif.addClass("active");
            $("#notificationMessage").text("Успешно изменено!");
            setTimeout(function () {
                notif.removeClass("active");
            }, 1000)
        }
    })
}

const CreateNote = function () {
    var data = $("#create-note-form").serialize();
    $.ajax({
        type: 'POST',
        url: '/Note/Create',
        data: data,
        success: function (result) {
            $("#noteModalBodyDiv").html(result);
            var isValid = $("#noteModalBodyDiv").find('[name="IsValid"]').val() == 'True';
            if (isValid) {
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
                        $("#noteModal").modal("hide");
                        $("#content-note").html(data);                    
                        ResetMenuHighlight();
                        $("#active-note").addClass('active');
                    }
                })
            }
        }
    })
}

const EditNote = function (status) {
    var data = $("#edit-note-form").serialize();
  
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/Note/Edit',
        data: data,
        success: function () {
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
        },
        error: function () {
            $("#error-edit-note").text("Заполните поля заметки");
        }

    })
}

const PutInCanNote = function (noteId, status) {
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

const ArchiveNote = function (noteId) {
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

const UnArchiveNote = function (noteId) {
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

const DeleteFromTrashCanNote = function (noteId) {
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

const RecoverFromTrashCanNote = function (noteId) {
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

const DeleteNote = function (noteId, status) {
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

const ClickOnCreate = function () {
    var innerArrow = document.getElementById("img");
    innerArrow.setAttribute("transform", "rotate(45)");
}

const Search = function (status) {
    var attribute = document.getElementById("input-search").value;
    let url = "/Note/GetNotesList?attribute=" + attribute + '&status=' + status;

    if (status == 0) {
        ResetMenuHighlight();
        $("#active-note").addClass('active');
    }

    $("#content-note").load(url, function () {

        })
}

const PinNote = function (noteId) {
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

const UnPinNote = function (noteId) {
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

const ResetMenuHighlight = function () {
    $(".nav-link.py-3.border-bottom").each(function () {
        $(this).removeClass('active');
    })
}
