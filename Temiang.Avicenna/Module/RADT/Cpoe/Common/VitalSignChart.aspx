<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="VitalSignChart.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.VitalSignChart" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartCtl.ascx" TagPrefix="uc1" TagName="VitalSignChartCtl" %>

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
            padding-top: 6px;
            padding-bottom: 6px;
            text-align: center;
            background-color: #4CAF50;
            color: white;
        }
    </style>
    <table width="100%">
        <tr>
            <td class="label">Show data from date</td>
            <td style="width:110px" >
                <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px"></telerik:RadDatePicker>
            </td>
            <td style="width:10px">&nbsp;To&nbsp;</td>
            <td style="width:110px" >
                <telerik:RadDatePicker runat="server" ID="txtToDate" Width="100px"></telerik:RadDatePicker>
            </td>
            <td style="text-align: left">
                <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnRefresh_Click" ToolTip="Search" OnClientClick="javascript:pageLoad();" />
            </td>
            <td></td>
        </tr>
    </table>
    <uc1:VitalSignChartCtl runat="server" ID="vitalSignChartCtl" />

</asp:Content>
