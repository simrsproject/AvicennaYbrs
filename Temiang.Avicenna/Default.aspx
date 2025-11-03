<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Temiang.Avicenna.Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .center-div {
            position: fixed;
            top: 50%;
            left: 50%; /* bring your own prefixes */
            transform: translate(-50%, -50%);
        }

    </style>
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            var BaseURL = '<%=BaseURL()%>';
            //$(document).ready(function () {
            //    LoadHomePage();
            //});

            function LoadHomePage() {
                $.ajax({
                    type: 'POST',
                    url: BaseURL + "/Home/GetToastData",
                    data: {},
                    success: function (data) {
                        //console.log(data);
                        if (data.status == "OK") {
                            toastr.options = {
                                "closeButton": true,
                                "positionClass": "toast-bottom-right",
                                "showEasing": "swing",
                                "hideEasing": "linear",
                                "showMethod": "fadeIn",
                                "hideMethod": "fadeOut",
                                "timeOut": 10000
                            }
                            //console.log(data.data);
                            for (var i = 0; i < data.data.length; i++) {
                                setTimeout(function (idx) {
                                    switch (data.data[idx].type) {
                                        case 0: {
                                            toastr.success(data.data[idx].msg);
                                            break;
                                        } case 1: {
                                            toastr.info(data.data[idx].msg);
                                            break;
                                        } case 2: {
                                            toastr.warning(data.data[idx].msg);
                                            break;
                                        } case 3: {
                                            toastr.error(data.data[idx].msg);
                                            break;
                                        }
                                    }
                                    
                                }, i * 1000, i);
                                
                            }
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //ShowError(xhr.responseText);
                    },
                    dataType: 'json'
                });

                // kasih delay
                setTimeout(function () { DoLoadHomePage(); }, 2000);
            }
            function DoLoadHomePage() {
                $.ajax({
                    type: 'POST',
                    url: BaseURL + "/Home/HomePage",
                    data: {},
                    success: function (data) {
                        //console.log(data);
                        if (!data.includes('empty_empty')) { // gini aja dulu deh, nanti diperbaiki
                            $('#divMain').html(data);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        ShowError(xhr.responseText);
                    },
                    dataType: 'html'
                });
            }
        </script>
    </telerik:RadCodeBlock>
    <div id="divMain">
        <div class="center-div">
            <asp:Image runat="server" ID="imgCompany" ImageUrl="~/Images/LogoRS_MainMenu.jpg" />
        </div>
    </div>
</asp:Content>
