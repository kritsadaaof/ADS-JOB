$(document).ready(function () {
    document.getElementById('GropEdit').style.display = 'none'; 
    $(".LINK").click(function () {
        document.getElementById('OPhev').checked = false; 
        document.getElementById('OPAds').checked = false; 
        document.getElementById('OPOth').checked = false; 
        $.post(baseUrl + "Account/CheckJOB", {

            ID: this.id
        }).done(function (data) {
            var pr = $.parseJSON(data);
            if (data != []) {
                document.getElementById('GropEdit').style.display = '';
                $("#ID").html(pr[0]["ID"]);
                $("#job-title").val(pr[0]["JOB_Title"]);
                $("#job-location").val(pr[0]["JOB_Location"]);
                $("#Experience").val(pr[0]["Experience"]);
                $("#Salary").val(pr[0]["Salary"]);
                $("#GenderDT").html(pr[0]["Gender"]);
                $("#RegionDT").html(pr[0]["JOB_Region"]);
                $("#job-typeDT").html(pr[0]["JOB_Type"]);
                $("#Job-description").html(pr[0]["JOB_Description"]);
                $("#Job-requirement").html(pr[0]["JOB_Requirement"]);
                if (pr[0]["Option_Chevron"] == "T") { 
                    document.getElementById('OPhev').checked = true;
                } 
                if (pr[0]["Option_Adisorn"] == "T") {
                    document.getElementById('OPAds').checked = true;
                }
                if (pr[0]["OPtion_Others"] == "T") {
                    document.getElementById('OPOth').checked = true;
                } 

                if (pr[0]["Status"] == "T") {
                    document.getElementById('Status').checked = true;
                } 

                $("#company-name").val(pr[0]["Company_Name"]);
            }
            else {
                var nFrom = "bottom";
                var nAlign = "center";
                var nIcons = $(this).attr('data-icon');
                var nType = "warning";
                var nAnimIn = $(this).attr('data-animation-in');
                var nAnimOut = $(this).attr('data-animation-out');
                var mEss = "ไม่พบข้อมูลนี้";
                notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss); 
            }
        });
    });
    $("#SavePostjobEdit").click(function () {
        //  alert($("#formUP").va([0]));
        var C = null, A = null, O = null, Status = "F";
        if (document.getElementById("OPhev").checked == true) {
            C = "T";
        }
        if (document.getElementById("OPAds").checked == true) {
            A = "T";
        }
        if (document.getElementById("OPOth").checked == true) {
            O = "T";
        }

        if (document.getElementById("Status").checked == true) {
            Status = "T";
        }
        if ($("#job-title").val() != ""
            && $("#job-location").val() != "" 
            && $("#Job-description").val() != ""
            && $("#Job-requirement").val() != "") {
            $.post(baseUrl + "Account/SavePostEdit", {
                ID: $("#ID").html(),
                JOBTITLE: $("#job-title").val(),
                JOBLOCATION: $("#job-location").val(),
                JOBREGION: $("#job-region").val(),
                JOBTYPE: $("#job-type").val(),
                JOBDES: $("#Job-description").val(),
                JOBRQ: $("#Job-requirement").val(),
                OPTIONc: C, OPTIONa: A, OPTIONo: O, STATUS: Status,
                COMPANY: $("#company-name").val(),
                EXPERIENCE: $("#Experience").val(),
                SALARY: $("#Salary").val(),
                GENDER: $("#Gender").val()

            }).done(function (data) {
                if (data == "S") {
                    UpImg();
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