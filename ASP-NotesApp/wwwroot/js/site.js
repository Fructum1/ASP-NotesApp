function Edit(noteId) {

    var url = "/Note/Edit?id=" + noteId;

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");

    })
}

function Create() {
    var url = "/Note/Create";

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");
    })
}

function ShowNotification(message) {
    var notif = $("#notificationContainer");
    $("#notificationMessage").text(message);
    notif.fadeIn();
    setTimeout(function () {
        notif.fadeOut();
    }, 1000)
}

function CreateNote() {
    var data = $("#create-note-form").serialize();
    $.ajax({
        type: 'POST',
        url: '/Note/Create',
        data: data,
        success: function (result) {
            $("#noteModalBodyDiv").html(result);
            var isValid = $("#noteModalBodyDiv").find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                ShowNotification("Успешно создано!");

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

function EditNote(status) {
    var data = $("#edit-note-form").serialize();
  
    console.log(data);
    $.ajax({
        type: 'POST',
        url: '/Note/Edit',
        data: data,
        success: function () {
                ShowNotification("Успешно изменено!");
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

function PutInCanNote(noteId, status) {
    $.ajax({
        url: "/Note/Delete?id=" + noteId,
        type: 'POST',

    }).done(function (e) {
        ShowNotification("Успешно удалено!");

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

function ArchiveNote(noteId) {
    $.ajax({
        url: "/Note/Archive?id=" + noteId,
        type: 'POST',
    }).done(function (e) {
        ShowNotification("Успешно архивировано!");
    
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



function UnArchiveNote(noteId) {
    $.ajax({
        url: "/Note/UnArchive?id=" + noteId,
        type: 'POST',
    }).done(function () {
        ShowNotification("Успешно извлечено!");

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

function DeleteFromTrashCanNote(noteId) {
    $.ajax({
        url: "/Note/DeleteFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        ShowNotification("Успешно удалено!");

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

function RecoverFromTrashCanNote(noteId) {
    $.ajax({
        url: "/Note/RecoverFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        ShowNotification("Успешно восстановлено!");

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

function DeleteNote(noteId, status) {
    $.ajax({
        url: "/Note/DeleteFromTrashCan?id=" + noteId,
        type: 'POST',
    }).done(function () {
        ShowNotification("Успешно удалено!");

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

function ClickOnCreate() {
    var innerArrow = document.getElementById("img");
    innerArrow.setAttribute("transform", "rotate(45)");
}

function Search(status) {
    var attribute = document.getElementById("input-search").value;
    let url = "/Note/GetNotesList?attribute=" + attribute + '&status=' + status;

    if (status == 0) {
        ResetMenuHighlight();
        $("#active-note").addClass('active');
    }

    $("#content-note").load(url, function () {

        })
}

function PinNote(noteId) {
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

function UnPinNote(noteId) {
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

function DeleteUser(userId) {
    var url = "/User/Delete?id=" + userId;

    $("#noteModalBodyDiv").load(url, function () {
        $("#noteModal").modal("show");
    })
}

function ConfirmDeleteUser(userId) {
    var url = "/User/DeleteConfirmed?id=" + userId;

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        success: function () {
            $("#noteModal").modal("hide");
            window.location.href = '/User/Login';
        }
    })
}

function ResetMenuHighlight() {
    $(".nav-link.py-3.border-bottom").each(function () {
        $(this).removeClass('active');
    })
}
