var objUser = {
    Id : 0,
    Email: "",
    Name: "",
    Telephone: ""
}

$(document).ready(function () {

    $('#txtTelephone').mask(SPMaskBehavior, spOptions);

    MaskTextField('#txtName');

    $('#btnSave').click(function () {
        if ($('#txtName').hasClass("valid") && $('#txtName').val().trim().indexOf(" ") >= 0) {
            if ($('#txtEmail').hasClass("valid")) {
                if (objUser.Id > 0) {
                    Update();
                }
                else {
                    Save();
                }
            }
            else {
                $('#txtEmail').focus();
            }
        }
        else {
            $('#txtName').addClass("invalid")
            $('#txtName').focus();

        }
    });

    $('#txtName').blur(function () {
        if ($('#txtName').val().trim().indexOf(" ") < 0) {
            $('#txtName').addClass("invalid");
        }
    });

    $('#txtEmail').blur(function () {
        if (!$('#txtEmail').hasClass("valid")) {
            $('#txtEmail').addClass("invalid");
        }
    });

    $('#btnNew').click(function () {
        ClearForm();
    });

    $('#btnCancel').click(function () {
        ClearForm();
    });

    $('#txtEmail, #txtName').characterCounter();

    GetList();
});

function GetList() {

    $('#divTable').html("Carregando...");

    $.ajax({
        async: true,
        type: 'GET',
        datatype: 'json',
        cache: false,
        url: '/User/GetList',
        data: { },
        success: function (result) {

            var expr = new RegExp('>[ \t\r\n\v\f]*<', 'g');
            var res = result.replace(expr, '><');
            $('#divTable').html(res);
            
        },
        error: function (xhr) {
            var responseTitle = $(xhr.responseText).filter('title').get(0);
            alert($(responseTitle).text()); 
            $('#divTable').empty();
        }
    });
}

function GetById(pId) {

    objUser.Id = pId;

    $.ajax({
        type: 'GET',
        datatype: 'json',
        cache: false,
        url: '/User/GetById',
        data: { pId: objUser.Id},
        success: function (result) {

            $('#txtEmail').val(result.Email).focus().blur();
            $('#txtName').val(result.Name).focus().blur();
            $('#txtTelephone').val(result.Telephone).focus().blur();

            $('#spnStatus').html("Editando o ID: " + objUser.Id);

        },
        error: function (xhr) {

            var responseTitle = $(xhr.responseText).filter('title').get(0);
            alert($(responseTitle).text()); 

        }
    });
}

function Save() {

    objUser.Email = $('#txtEmail').val();
    objUser.Name = $('#txtName').val();
    objUser.Telephone = $('#txtTelephone').val();


    $.ajax ({
        type: 'POST',
        datatype: 'json',
        cache: false,
        url: '/User/Save',
        data: objUser,
        success: function (result) {

            GetList();
            ClearForm();
            alert(result.Message);

        },
        error: function (xhr) {

            var responseTitle = $(xhr.responseText).filter('title').get(0);
            alert($(responseTitle).text());

        }
    });

}

function Update() {

    objUser.Email = $('#txtEmail').val();
    objUser.Name = $('#txtName').val();
    objUser.Telephone = $('#txtTelephone').val();


    $.ajax({
        type: 'POST',
        datatype: 'json',
        cache: false,
        url: '/User/Update',
        data: objUser,
        success: function (result) {

            GetList();
            ClearForm();
            alert(result.Message);

        },
        error: function (xhr) {

            var responseTitle = $(xhr.responseText).filter('title').get(0);
            alert($(responseTitle).text());

        }
    });
}

function DeleteById(pId) {
    $.ajax({
        type: 'POST',
        datatype: 'json',
        cache: false,
        url: '/User/DeleteById',
        data: { pId: pId },
        success: function (result) {

            GetList();
            ClearForm();
            alert(result.Message);

        },
        error: function (xhr) {

            var responseTitle = $(xhr.responseText).filter('title').get(0);
            alert($(responseTitle).text());

        }
    });
}

function ClearForm() {

    $('#txtEmail').val('').focus().blur().removeClass("valid invalid");
    $('#txtName').val('').focus().blur().removeClass("valid invalid");
    $('#txtTelephone').val('').focus().blur();

    objUser.Id = 0;
    objUser.Email = "";
    objUser.Name = "";
    objUser.Telephone = "";

    $('#txtName').val('').focus();

    $('#spnStatus').html("Cadastrando novo usuário");


    $('.character-counter').empty();

    M.updateTextFields();
}
