<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="VitalSignChartAnesthesia.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.VitalSignChartAnesthesia" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #ewsscore {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #ewsscore td, #medused th {
                border: 1px solid #a9a9a9;
                padding: 4px;
                text-align: center;
            }

            #ewsscore tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #ewsscore tr:hover {
                background-color: #ddd;
            }

            #ewsscore th {
                border: 1px solid #a9a9a9;
                padding-top: 6px;
                padding-bottom: 6px;
                text-align: center;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <table width="100%">
        <tr>
            <td style="width: 450px">
                <table width="100%">
                    <tr>
                        <td class="label" style="width: 40px">Date</td>
                        <td style="width: 110px">
                            <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px"></telerik:RadDatePicker>
                        </td>
                        <td style="width: 40px">
                            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button runat="server" ID="btnStartFromRegistration" Text="Registration Date" OnClick="btnStartFromRegistration_Click" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button runat="server" ID="btnLastVitalSign" Text="Last Vital Sign Date" OnClick="btnLastVitalSign_Click" />
                        </td>
                    </tr>
                    <td></td>
                </table>
            </td>
            <td >
                <fieldset width="100%">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 40px">Name</td>
                            <td >:&nbsp;<asp:Label runat="server" ID="lblPatientName"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Sex</td>
                            <td style="width: 20px">:&nbsp;<asp:Label runat="server" ID="lblSex"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">DOB</td>
                            <td style="width: 90px">:&nbsp;<asp:Label runat="server" ID="lblBirthDate"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Age</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblAge"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <fieldset>
        <legend>Score Total</legend>
    <%=EwsTotalScoreLevelHtml() %>
    </fieldset>
    <br/>
    <asp:Panel runat="server" id="pnlChart" Width="100%"></asp:Panel>
</asp:Content>
