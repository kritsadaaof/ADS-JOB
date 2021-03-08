 

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


    $(".Link").click(function () {
        //alert(this.title);

        $('#tab_logic td').remove();
        $("#Title").html("ตำแหน่ง : " + this.title);
        document.getElementById('Regist').style.display = '';
        $.post(baseUrl + "ManageCandidates/LoadRegistJoblistByPosi", {
            Position: this.title
        }).done(function (data) {
            // alert(data);
            var pr = $.parseJSON(data);
            // alert(pr);
            $.each(JSON.parse(data), function (i, obj) {

                $('#tab_logic').append('<tr class="" id="tr' + pr[i]["ID"] + '">' +
                    '<td><div class="border p-1 d-inline-block mr-3 rounded"><img src="../Content/images/DOC/' + pr[i]["Pic_File"] + '" alt="No Image" width="100"></div></td>' +
                    '<td>' + pr[i]["Re_Title_EN"] + pr[i]["Re_Name_EN"] + "  " + pr[i]["Re_Surname_EN"] + '<br/>' + pr[i]["Re_Title_TH"] + pr[i]["Re_Name_TH"] + "  " + pr[i]["Re_Surname_TH"] + '<br/>' +
                    'ประสบการณ์ : ' + pr[i]["Exp"] + ' ปี <br/>วันที่สมัคร : ' + pr[i]["Create_Date"] + '</td>' +
                    '<td><a target="_blank" href="' + pr[i]["VC_File"] + '">คลิก..เพื่อดู</a></td>' +
                    '<td>' + (pr[i]["Cer_File"] != null?  "<a target='blank' href='" + pr[i]["Cer_File"] + "'>คลิก..เพื่อดู</a>":"ไม่ได้แนบ")+'</td>' +
                    '<td>' + (pr[i]["Status"] == "Regis" ? "<i style='color:blue'>สมัครใหม่</i>" : (pr[i]["Status"] == "Conf" ? "ผ่านการสัมภาษณ์":"นัดสัมภาษณ์")) + '</td>' +
                    '<td>' + (pr[i]["Status_Appointment"] == null ? "<input type='checkbox' class='x' value='" + pr[i]["ID"] + "'><a style=''> คลิก</a>" : "<input type='checkbox' class='x' value='" + pr[i]["ID"] + "'checked>") + '</td>' +
                    '<td>' + pr[i]["Date"] + '</td>' +
                    '<td>' + (pr[i]["Status_Confirm"] == "T" ? "<input type='checkbox' class='s' title='" + pr[i]["ID"] + "' value='T' checked><a> ผ่าน</a><br/>" : "<input type='checkbox' class='s' title='" + pr[i]["ID"] + "' value='T'><a> ผ่าน</a><br/>")+                   
                    '<input type="checkbox" class="s" title=' + pr[i]["ID"] + ' value="F"><a style=""> ไม่ผ่าน</a></td>' +
                    '</tr >');

                /*
                 <div class="border p-2 d-inline-block mr-3 rounded"><img src="~/Content/images/LOGO/@PostSing.Logo_Image" alt="No Image" width="100"></div>
                 * /


                /* $('#tab_logic').append('<tr class="FontSize" id="addr' + (List) + '"><td>' + (List) + '</td><td>' + pr[i]["JOB_Title"] + '</td><td>' + pr[i]["JOB_Region"] +
                     '</td><td>' + pr[i]["JOB_Location"] + '</td><td>' + pr[i]["Salary"] + '</td><td>' + " <div class='input-group'><input type='text'class='form-control py-2 px-4' id='Ex" +
                     List + "'  placeholder='-'><input type='text' value=" + pr[i]["ID"] + " id='Job" +
                     List + "'  placeholder='-' hidden><input type='text' value=" + pr[i]["JOB_Region"] + " id='Dep" +
                     List + "' hidden><Label id='Pos" +
                     List + "' hidden>" + pr[i]["JOB_Title"] + "</Label><div class='input-group-append FontSize'><span class='input-group-text px-2'>ปี</span></div></div>" + '</td></tr>');
 */
                List++;
            });
            Check();
            Check1();
        });

    });


    $("#formUPCV").click(function () {
        $("#exampleModalLong").modal('show');
    });

    $("#OK").click(function () { 
        $.post(baseUrl + "ManageCandidates/ChangApp", {
            ID: $("#IDHiddeb").val(),
            DATE: $("#AppDate").val(),
            TIME: $("#AppTime").val(),
            USERAPP: $("#UserApp").val(),
            DETAIL: $("#Detail").val()
        }).done(function (data) {
            if (data == "S") {
                var nFrom = "bottom";
                var nAlign = "center";
                var nIcons = $(this).attr('data-icon');
                var nType = "success";
                var nAnimIn = $(this).attr('data-animation-in');
                var nAnimOut = $(this).attr('data-animation-out');
                var mEss = "นัดสัมภาษณ์พร้อมส่ง Email แจ้งผู้สมัครเรียบร้อย";
                var Time = 5000;
                notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss, Time);
                setTimeout(
                    function () {
                        location.reload();
                    }, 5000);
            }
        });
    });



});
function dateTime(A) {
    alert(A);
    var dt = new Date(moment(A));
    var month = '' + (dt.getMonth() + 1), day = '' + dt.getDate(), year = dt.getFullYear();
    var Hou = '' + dt.getHours(), Min = '' + dt.getMinutes(), Sec = '' + dt.getSeconds();
    if (Hou.length < 2) Hou = '0' + Hou;
    if (Min.length < 2) Min = '0' + Min;
    if (Sec.length < 2) Sec = '0' + Sec;
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    // var times = ;
    var val = day + "/" + month + "/" + year + "  " + Hou + ":" + Min + " น.";
    return val;
}
function Check() {
    $(".x").on('click', function () {
        if (this.checked) {
            this.checked = false;
            $("#IDHiddeb").val($(this).val());
            $("#exampleModalLong").modal('show'); 
        }
        else { 
        }
    });
}
function Check1() {
    $(".s").on('click', function () {  
        if (this.checked) {
            if ($(this).val() == "T") {
                var r = confirm("ยืนยัน ผ่าน");
                if (r == true) {
                    $.post(baseUrl + "ManageCandidates/ChangConf", {
                        ID: this.title,
                        STATUS: "T"                        
                    });

                } else { this.checked = false; }
            }
            else if ($(this).val() == "F") {
                var row = document.getElementById('tr'+this.title);
                row.parentElement.removeChild(row); 

                var r = confirm("ยืนยัน ไม่ผ่าน");
                if (r == true) {
                    $.post(baseUrl + "ManageCandidates/ChangConf", {
                        ID: this.title,
                        STATUS: "F"
                    }).done(function (data) {
                     //   alert($("#" + data).html());
                        var a = $("#" + data).html() - 1;
                      //  alert(a);
                        $("#" + data).html(a);
                    });
                }
                else { this.checked = false;}
            }
           // alert($(this).val());
           // alert(this.title);  
        }
        else {
        }
    });
}
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
