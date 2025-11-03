<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearSemesterCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.YearSemesterCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Semester" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadComboBox ID="cboPeriodSemester" Width="100px" runat="server">
                        </telerik:RadComboBox>
                    </td>
                    <td style="width: 5px"></td>
                    <td style="width: 35px">
                        <asp:Label ID="lblYear" runat="server" Text="Year" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboPeriodYear" Width="70px" runat="server" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
