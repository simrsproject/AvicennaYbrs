<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationAnesthesiaHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationAnesthesiaHist" %>

<%@ Register TagPrefix="cc" TagName="MedicationCtl" Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Medication/MedicationCtl.ascx" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHistCtl.ascx" TagPrefix="cc" TagName="MedicationHistCtl" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #medused {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #medused td, #medused th {
                border: 1px solid #a9a9a9;
                padding: 4px;
            }

            #medused tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #medused tr:hover {
                background-color: #ddd;
            }

            #medused th {
                padding-top: 6px;
                padding-bottom: 6px;
                text-align: center;
                background-color: #4CAF50;
                color: white;
            }

        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 100px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -40px;
                opacity: 0;
                transition: opacity 0.3s;
            }

                .tooltip .tooltiptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 50%;
                    margin-left: -5px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: #555 transparent transparent transparent;
                }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }
    </style>
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function openWinEntry(url, width, height) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width, height);
            }

            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) -
                    document.getElementById('tblHeader').offsetHeight - 36;
                var grid;

                // set height to the whole RadGrid control
                grid = $find("<%= medicationHistCtl.GridClientID %>");
                grid.get_element().style.height = height + "px";
                grid.repaint();
            }

            function showPrescription(prescno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/Prescription.aspx?prescno=' + prescno;
                openWinEntry(url, 600, 550);
            }

            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRefresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table id="tblHeader" style="width: 100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label" style="width: 40px">Date</td>
                        <td style="width: 110px">
                            <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px"></telerik:RadDatePicker>
                        </td>

                        <td style="width: 80px">
                            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td class="label" style="width: 100px">Start From</td>
                        <td style="width: 120px">
                            <asp:Button runat="server" ID="btnStartFromRegistration" Text="Operating Date" OnClick="btnStartFromRegistration_Click" Width="120px" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <fieldset width="100%">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 40px">Name</td>
                            <td style="width: 200px">:&nbsp;<asp:Label runat="server" ID="lblPatientName"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">Sex</td>
                            <td style="width: 10px">:&nbsp;<asp:Label runat="server" ID="lblSex"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">DOB</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblBirthDate"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">Age</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblAge"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <cc:MedicationHistCtl runat="server" ID="medicationHistCtl" IsConsumeMethodChangeAble="False" />
</asp:Content>
