<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RenkinDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.RenkinDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="uc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblRenkinID" runat="server" Text="Renkin ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtRenkinID" runat="server" Width="300px" NumberFormat-DecimalDigits="0" MaxLength="10" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPositionID" runat="server" Text="Position ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionID_ItemDataBound"
                    OnItemsRequested="cboPositionID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PositionCode")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position ID required."
                    ValidationGroup="entry" ControlToValidate="cboPositionID"
                    SetFocusOnError="True" Width="100%">
						<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblKegiatan" runat="server" Text="Kegiatan"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtKegiatan" runat="server" Width="300px" MaxLength="300" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvKegiatan" runat="server" ErrorMessage="Kegiatan required."
                    ValidationGroup="entry" ControlToValidate="txtKegiatan"
                    SetFocusOnError="True" Width="100%">
						<asp:Image runat="server" SkinID="rfvImage"/>
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
       <tr>
            <td class="label">
                <asp:Label ID="lblSRRenkinJenisKegiatan" runat="server" Text="Renkin Jenis Kegiatan"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadComboBox ID="cboSRRenkinJenisKegiatan" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRRenkinJenisKegiatan" runat="server" ErrorMessage="Renkin Jenis Kegiatan required."
                    ValidationGroup="entry" ControlToValidate="cboSRRenkinJenisKegiatan" 
					SetFocusOnError="True" Width="100%">
						<asp:Image runat="server" SkinID="rfvImage"/>
				</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTargetPersen" runat="server" Text="Target Persen"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtTargetPersen" runat="server" NumberFormat-DecimalDigits="0" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTargetPersen" runat="server" ErrorMessage="Target Persen required."
                    ValidationGroup="entry" ControlToValidate="txtTargetPersen" 
					SetFocusOnError="True" Width="100%">
						<asp:Image runat="server" SkinID="rfvImage"/>
				</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTargetBulan" runat="server" Text="Target Bulan"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtTargetBulan" runat="server" NumberFormat-DecimalDigits="0" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTargetBulan" runat="server" ErrorMessage="Target Bulan required."
                    ValidationGroup="entry" ControlToValidate="txtTargetBulan" 
					SetFocusOnError="True" Width="100%">
						<asp:Image runat="server" SkinID="rfvImage"/>
				</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>



