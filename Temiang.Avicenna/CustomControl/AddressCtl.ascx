<%@ Control Language="C#" AutoEventWireup="true" Codebehind="AddressCtl.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.AddressCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="2" style="text-align: center">
            <table width="100%">
                <tr>
                    <td class="labelcaption">
                        <asp:Label ID="Label1" runat="server" Text="Address"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="50%" style="vertical-align: text-top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStreetName" runat="server" Text="Street Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtStreetName" runat="server" Width="300px" MaxLength="250" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStreetName" runat="server" ErrorMessage="Street Name required."
                            ValidationGroup="entry" ControlToValidate="txtStreetName" SetFocusOnError="True" Width="20px"
                            Visible="False">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboZipCode" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboZipCode_ItemDataBound"
                            OnItemsRequested="cboZipCode_ItemsRequested" OnSelectedIndexChanged="cboZipCode_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "District")%>
                                &nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ZipPostalCode")%>) </b>
                                <br />
                                County :
                                <%# DataBinder.Eval(Container.DataItem, "County")%>
                                <br />
                                City :
                                <%# DataBinder.Eval(Container.DataItem, "City")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ErrorMessage="Zip Code required."
                            ValidationGroup="entry" ControlToValidate="cboZipCode" SetFocusOnError="True" Width="20px"
                            Visible="False">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDistrict" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCounty" runat="server" Text="County"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCounty" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCity" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtState" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ErrorMessage="Phone No required."
                            ValidationGroup="entry" ControlToValidate="txtPhoneNo" SetFocusOnError="True" Width="20px"
                            Visible="False">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFaxNo" runat="server" Text="Fax No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtFaxNo" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMobilePhoneNo" runat="server" Text="Mobile Phone No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMobilePhoneNo" runat="server" Width="300px" MaxLength="20">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMobilePhoneNo" runat="server" ErrorMessage="Mobile Phone No required."
                            ValidationGroup="entry" ControlToValidate="txtMobilePhoneNo" SetFocusOnError="True" Width="20px"
                            Visible="False">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is not valid" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="entry">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
