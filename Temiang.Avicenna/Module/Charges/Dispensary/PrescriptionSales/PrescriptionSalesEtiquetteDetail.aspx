<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionSalesEtiquetteDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionSalesEtiquetteDetail" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtReferenceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtReferenceNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtBatchNumber" />
                    <telerik:AjaxUpdatedControl ControlID="txtExpiredDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            function openWinItemEdList() {
                var oWnd = $find("<%= winPOR.ClientID %>");
                var oit = $find("<%= txtItemID.ClientID %>");
                oWnd.setUrl('PrescriptionSalesEtiquetteDetailEdList.aspx?itemId=' + oit.get_value());
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                var txtRefNo = $find("<%= txtReferenceNo.ClientID %>");

                if (oWnd.argument)
                    txtRefNo.set_value(oWnd.argument.tno);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winPOR" Animation="None" Width="900px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false" Modal="true"
        OnClientClose="onClientClose" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="225"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item name required."
                                ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblConsumeMethod" runat="server" Text="Consume Method"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtConsumeMethod" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvConsumeMethod" runat="server" ErrorMessage="Consume method required."
                                ValidationGroup="entry" ControlToValidate="txtConsumeMethod" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Keeping"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKeeping" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Spesific Info"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSpesificInfo" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSelectBatchNumber">
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text=">> Select Batch Number" Font-Italic="true" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="true" ForeColor="Gray"/>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="197px" MaxLength="50" AutoPostBack="true" OnTextChanged="txtReferenceNo_TextChanged"
                                            ClientEvents-OnButtonClick="openWinItemEdList" ForeColor="Gray" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBatchNumber" runat="server" Text="Batch Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBatchNumber" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblExpiredDate" runat="server" Text="Expired Date [dd/MM/yyyy]"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDrugOutside" runat="server" Text="Drug Outside" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumberOfCopies" runat="server" Text="Number of Copies"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumberOfCopies" runat="server" Width="100px" Value="1"
                                NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
