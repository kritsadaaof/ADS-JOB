$(document).ready(function () { 
    $("#SavePostjob").click(function () {
        var C, A, O;
        if (document.getElementById("OPhev").checked == true) {
            C = "T";
        }
        if (document.getElementById("OPAds").checked == true) {
            A = "T";
        }
        if (document.getElementById("OPOth").checked == true) {
            O = "T";
        }
        if ($("#job-title").val() != ""
            && $("#job-location").val() != ""
            && $("#job-region").val() != ""
            && $("#Job-description").val() != ""
            && $("#Job-requirement").val() != "") {
            $.post(baseUrl + "PostJob/SavePostjob", {

                JOBTITLE: $("#job-title").val(),
                JOBLOCATION: $("#job-location").val(),
                JOBREGION: $("#job-region").val(),
                JOBTYPE: $("#job-type").val(),
                JOBDES: $("#Job-description").val(),
                JOBRQ: $("#Job-requirement").val(),
                OPTIONc: C, OPTIONa: A, OPTIONo: O,
                COMPANY: $("#company-name").val(),
                EXPERIENCE: $("#Experience").val(),
                SALARY: $("#Salary").val(),
                GENDER: $("#Gender").val(),
                ImgUplode: $("#ImgUplode").val()

            }).done(function (data) {
                if (data == "S") {
                    if ($("#ImgUplode").val() != "") {
                        UpImg();
                    }
                    var nFrom = "bottom";
                    var nAlign = "center";
                    var nIcons = $(this).attr('data-icon');
                    var nType = "success";
                    var nAnimIn = $(this).attr('data-animation-in');
                    var nAnimOut = $(this).attr('data-animation-out');
                    var mEss = "บันทึกข้อมูลสำเร็จ";
                    notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
                    setTimeout(
                        function () {
                            location.reload();
                        }, 4000);
                    // setInterval(window.location = baseUrl + "PostJob/Post_JOB", 10000);
                }
            });
        }
        else {
            var nFrom = "bottom";
            var nAlign = "center";
            var nIcons = $(this).attr('data-icon');
            var nType = "warning";
            var nAnimIn = $(this).attr('data-animation-in');
            var nAnimOut = $(this).attr('data-animation-out');
            var mEss = "กรุณากรอกข้อมูลให้ครบ";
            notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
          
        }
    });
});

function UpImg() {
    var formData = new FormData($("#formUP")[0]);
    $.ajax({
        type: 'POST',
        url: "../PostJob/UploadFiles",
        data: formData,
        cache: false,
        contentType: false,
        processData: false
    });
}

function notify(from, align, icon, type, animIn, animOut, mEssage) { //Notify
    $.growl({
        icon: icon,
        title: ' แจ้งเตือน ',
        message: mEssage,

        url: ''
    }, {
        element: 'body',
        type: type,
        allow_dismiss: true,
        placement: {
            from: from,
            align: align
        },
        offset: {
            x: 20,
            y: 85
        },
        spacing: 10,
        z_index: 1031,
        delay: 2500,
        timer: 2000,
        url_target: '_blank',
        mouse_over: false,
        animate: {
            enter: animIn,
            exit: animOut
        },
        icon_type: 'class',
        template: '<div data-growl="container" class="alert" role="alert">' +
            '<button type="button" class="close" data-growl="dismiss">' +
            '<span aria-hidden="true">&times;</span>' +
            '<span class="sr-only">Close</span>' +
            '</button>' +
            '<span data-growl="icon"></span>' +
            '<span data-growl="title"></span>' +
            '<span data-growl="message"></span>' +
            '<a href="#" data-growl="url"></a>' +
            '</div>'
    });
};

/*
function notify(from, align, icon, type, animIn, animOut, mEssage) { //Notify
    $.growl({
        icon: icon,
        title: ' แจ้งเตือน ',
        message: mEssage,

        url: ''
    }, {
        element: 'body',
        type: type,
        allow_dismiss: true,
        placement: {
            from: from,
            align: align
        },
        offset: {
            x: 20,
            y: 10
        },
        spacing: 10,
        z_index: 1031,
        delay: 2500,
        timer: 2000,
        url_target: '_blank',
        mouse_over: false,
        animate: {
            enter: animIn,
            exit: animOut
        },
        icon_type: 'class',
        template: '<div data-growl="container" class="alert" role="alert">' +
            '<button type="button" class="close" data-growl="dismiss">' +
            '<span aria-hidden="true">&times;</span>' +
            '<span class="sr-only">Close</span>' +
            '</button>' +
            '<span data-growl="icon"></span>' +
            '<span data-growl="title"></span>' +
            '<span data-growl="message"></span>' +
            '<a href="#" data-growl="url"></a>' +
            '</div>'
    });
};
*/