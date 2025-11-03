<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ParameterDetail.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.ParameterDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParameterID" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvParameterID" runat="server" ErrorMessage="ID required."
                    ControlToValidate="txtParameterID" ValidationGroup="entry" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParameterName" runat="server" Width="100%" TextMode="MultiLine" Height="100px">
                </telerik:RadTextBox>
            </td>
            <td width="20px" align="left">
                <asp:RequiredFieldValidator ID="rfvParameterName" runat="server" ErrorMessage="Description required."
                    ControlToValidate="txtParameterName" ValidationGroup="entry" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Value"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParameterValue" runat="server" Width="100%" TextMode="MultiLine" Height="100px" >
                    
                </telerik:RadTextBox>

            </td>
            <td width="20px" align="left">
            </td>
            <td></td>
        </tr>
    </table>
<%--    <script language="javascript" type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script type="text/javascript">
        $("textarea").each(function () {
            this.setAttribute("style", "height:" + (this.scrollHeight) + "px;overflow-y:hidden;");
        }).on("input", function () {
            this.style.height = "auto";
            this.style.height = (this.scrollHeight) + "px";
        });

    </script>--%>

</asp:Content>
