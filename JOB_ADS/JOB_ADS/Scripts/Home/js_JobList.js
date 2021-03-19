$(document).ready(function () {
    var List = 1;
    //     document.getElementById('CheckJobList').style.display = 'none';
    //   document.getElementById('Regist').style.display = '';


    $('#data_4 .input-group.date').datepicker({
        format: 'dd/mm/yyyy',
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });


    /////////////////////เงื่อนไขแสดง Value ของไฟล์ที่เลือก//////////////////////////////
    $('#CVUplode').change(function () {
        if ($('#CVUplode').val() == "") {
            $('#CV').html('Browse CV');
        } else {
            var res = $('#CVUplode').val().substring(12);
            $('#CV').html(res);
        }
    });
    $('#PICUplode').change(function () {
        if ($('#PICUplode').val() == "") {
            $('#PIC').html('Browse Pic');
        }
        else {
            var res = $('#PICUplode').val().substring(12);
            $('#PIC').html(res);
        }
    });
    $('#CERUplode').change(function () {
        if ($('#CERUplode').val() == "") {
            $('#CER').html('Browse Certificate');
        }
        else {
            var res = $('#CERUplode').val().substring(12);
            $('#CER').html(res);
        }
    });

    $('#Email').change(function () {
        validate();
    });
    // $("#Email").on("check", validate); 

    //////////////////////////////// บันทึกข้อมูล /////////////////////////////////////
    $("#SavePostjob").click(function () {
        // document.getElementById("SavePostjob").disabled = true;
        var Check = "";
        if ($("#NameEN").val() == "") {
            Check += " -  Name(En) </br>"
        }
        if ($("#SurnameEN").val() == "") {
            Check += " -  Surname(En) </br>"
        }
        if ($("#NameTH").val() == "") {
            Check += " -  ชื่อ(ไทย) </br>"
        }
        if ($("#SurnameTH").val() == "") {
            Check += " -  นามสกุล(ไทย) </br>"
        }
        if ($("#BDate").val() == "") {
            Check += " -  วัน/เดือน/ปี เกิด </br>"
        }
        if ($("#Phone").val() == "") {
            Check += " -  เบอร์โทรศัพท์ </br>"
        }
        if ($("#Email").val() == "") {
            Check += " -  E-Mail </br>"
        }
        if ($("#CVUplode").val() == "") {
            Check += " -  เลือกไฟล์ CV </br>"
        }
        if ($("#PICUplode").val() == "") {
            Check += " -  เลือกไฟล์รูปภาพ </br>"
        }
        //  if ($("#CERUplode").val() == "") {
        //      Check += " -  เลือกไฟล์Certificate </br>"
        //  }
        //  if ($("#Address").val() == "") {
        //    Check += " -  ข้อมูลที่อยู่ </br>"
        //} 

        if (Check != "") {
            // document.getElementById("SavePostjob").disabled = false;
            var nFrom = "top";
            var nAlign = "center";
            var nIcons = $(this).attr('data-icon');
            var nType = "warning";
            var nAnimIn = $(this).attr('data-animation-in');
            var nAnimOut = $(this).attr('data-animation-out');
            var mEss = "กรุณากรอกข้อมูลให้ครบ </br> " + Check;
            var Time = 5000;
            notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss, Time);
        }
        else if (Check == "") {
            $("#Agree").on('click', function () {
                if (this.checked) {
                    $("#ModalLongSensitive").modal('hide');
                }
                else {
                }
            });
            if (document.getElementById("Agree").checked == true) {

                document.getElementById("SavePostjob").disabled = true;
                var Check = "";
                var Agree = "Yes";
                $.ajax({
                    type: 'POST',
                    data: new FormData($("#formUPCV")[0]),
                    url: "../Home/UploadFiles",
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (datacv) {
                        $.ajax({
                            type: 'POST',
                            data: new FormData($("#formUPPIC")[0]),
                            url: "../Home/UploadFiles",
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (datapic) {
                                $.ajax({
                                    type: 'POST',
                                    data: new FormData($("#formUPCER")[0]),
                                    url: "../Home/UploadFiles",
                                    cache: false,
                                    contentType: false,
                                    processData: false,
                                    success: function (datacer) {
                                        $.ajax({
                                            type: 'POST',
                                            data: new FormData($("#formUPCERWORK")[0]),
                                            url: "../Home/UploadFiles",
                                            cache: false,
                                            contentType: false,
                                            processData: false,
                                            success: function (datacerwork) {
                                                for (i = 1; i <= List; i++) {
                                                    $.post(baseUrl + "Home/SaveRegist", {
                                                        TitleEN: $("#TitleEN").val(),
                                                        NameEN: $("#NameEN").val(),
                                                        SurnameEN: $("#SurnameEN").val(),
                                                        TitleTH: $("#TitleTH").val(),
                                                        NameTH: $("#NameTH").val(),
                                                        SurnameTH: $("#SurnameTH").val(),
                                                        BDate: $("#BDate").val(),
                                                        Sex: $("#Sex").val(),
                                                        Phone: $("#Phone").val(),
                                                        Email: $("#Email").val(),
                                                        PositionOld: $("#PositionOld").val(),
                                                        Address: $("#Address").val(),
                                                        datacv: datacv,
                                                        datapic: datapic,
                                                        datacer: datacer,
                                                        datacerwork: datacerwork,
                                                        Agree: Agree,
                                                        Experience: $("#Ex" + i).val(),
                                                        JobID: $("#Job" + i).val(),
                                                        Dep: $("#Dep" + i).val(),
                                                        Pos: $("#Pos" + i).val(),
                                                        Mail_User: $("#DetailUserSenmail").val()
                                                    });
                                                }
                                                var nFrom = "bottom";
                                                var nAlign = "center";
                                                var nIcons = $(this).attr('data-icon');
                                                var nType = "success";
                                                var nAnimIn = $(this).attr('data-animation-in');
                                                var nAnimOut = $(this).attr('data-animation-out');
                                                var mEss = "บันทึกข้อมูลสำเร็จ";
                                                var Time = 2000;
                                                notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss, Time);
                                                setTimeout(
                                                    function () {
                                                        location.reload();
                                                    }, 4000);
                                            }
                                        });
                                    }
                                });
                            }
                        });
                    }
                });
            } else { $("#ModalLongSensitive").modal('show'); }
        }


    });

    function validateEmail(email) {
        const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function validate() {
        const $result = $("#result");
        const email = $("#Email").val();
        $result.text("");

        if (validateEmail(email)) {
            $result.text("");
            $result.css("color", "green");
        } else {
            $result.text("อีเมล์ไม่ถูกต้อง");
            $result.css("color", "red");
            $("#Email").val("").focus();
        }
        return false;
    }

    function Up(formData) {
        //alert(Name) 
        $.ajax({
            type: 'POST',
            data: formData,
            url: "../Home/UploadFiles",
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
            }
        });
    }

    function Ups(formData, Name) {
        alert(Name);
        $.post(baseUrl + "Home/UploadFiles", {
            formData: formData,
            Name: Name,
            cache: false,
            contentType: false,
            processData: false
        });
    }

    $("#Clear").click(function () {
        location.reload();
        //  alert($("#Job1").val());
    });

    $("#OK").click(function () {
        $("#CVUplode").click();
    });

    $("#formUPCV").click(function () {
        $("#exampleModalLong").modal('show');
    });


    $("#ApplyJobList").click(function () {

        //  $('#tab_logic').append('<tr id="addr' + (1) + '"><td>aaa</td></tr>');
        //  document.getElementById('FormPDConfirm').style.display = '';
        //  alert(x);
        // window.location = baseUrl + "Home/JobList?JOB=" + $('#TitleS').val();


        /////////////////////////ดึงค่าจาก Checkbox ที่เลือก///////////////////////////
        const checkboxes = document.querySelectorAll('.z:checked');
        let colors = [];

        checkboxes.forEach((checkbox) => {
            colors.push(checkbox.value);

            /////////////////////////////ข้อมูลตำแหน่งที่เลือก////////////////////////////////////
            $.post(baseUrl + "Home/LoadRegistJoblist", {
                IDJob: checkbox.value
            }).done(function (data) {
                // alert(data);
                var pr = $.parseJSON(data);
                // alert(pr);
                $.each(JSON.parse(data), function (i, obj) {

                    $('#tab_logic').append('<tr class="FontSize" id="addr' + (List) + '"><td>' + (List) + '</td><td>' + pr[i]["JOB_Title"] + '</td><td>' + pr[i]["JOB_Region"] +
                        '</td><td>' + pr[i]["JOB_Location"] + '</td><td>' + pr[i]["Salary"] + '</td><td>' + " <div class='input-group'><input type='text'class='form-control py-2 px-4' id='Ex" +
                        List + "'  placeholder='-'><input type='text' value=" + pr[i]["ID"] + " id='Job" +
                        List + "'  placeholder='-' hidden><input type='text' value=" + pr[i]["JOB_Region"] + " id='Dep" +
                        List + "' hidden>                 <input type='text' value=" + pr[i]["JOB_Title"] + " id='Pos" +
                        List + "' hidden> <div class='input-group-append FontSize'><span class='input-group-text px-2'>ปี</span></div></div>" + '</td></tr>');
                    // a = a + 1;
                });
                List++;
            });
        });
        ///////////////////////////////////////////////////////////////////// 
        if (colors != '') {
            document.getElementById('CheckJobList').style.display = 'none';
            document.getElementById('Regist').style.display = '';
        }
    });
});

function notify(from, align, icon, type, animIn, animOut, mEssage, Time) { //Notify
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
            y: 60
        },
        spacing: 10,
        z_index: 1031,
        delay: 2500,
        timer: Time,
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
