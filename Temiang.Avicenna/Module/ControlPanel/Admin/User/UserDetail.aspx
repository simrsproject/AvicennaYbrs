<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="UserDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Admin.UserDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="UserDetail.js" type="text/javascript"></script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUserID" runat="server" Text="User ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUserID" runat="server" Width="100px" MaxLength="40" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="User ID required."
                                ValidationGroup="entry" ControlToValidate="txtUserID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUserName" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="User Name required."
                                ValidationGroup="entry" ControlToValidate="txtUserName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="User Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRUserType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="User type required."
                                ValidationGroup="entry" ControlToValidate="cboSRUserType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit (Main)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRLanguage" runat="server" Text="Language"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRLanguage" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRLanguage" runat="server" ErrorMessage="Language ID required."
                                ValidationGroup="entry" ControlToValidate="cboSRLanguage" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLicenseNo" runat="server" Text="License No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLicenseNo" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="ESign NIK"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtESignNik" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Employee No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                OnItemsRequested="cboPersonID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPassword" runat="server" Width="300px" MaxLength="15"
                                TextMode="Password" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPasswordConfirm" runat="server" Text="Password Confirm"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPasswordConfirm" runat="server" Width="300px" MaxLength="15"
                                TextMode="Password" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblActiveDate" runat="server" Text="Active Date"></asp:Label>
                        </td>

                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtActiveDate" runat="server" Width="100px" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfvActiveDate" runat="server" ErrorMessage="Active Date required."
                                            ValidationGroup="entry" ControlToValidate="txtActiveDate" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td class="label" style="width: 70px">
                                        <asp:Label ID="lblExpireDate" runat="server" Text="Expire Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtExpireDate" runat="server" Width="100px" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfvExpireDate" runat="server" ErrorMessage="Expire Date required."
                                            ValidationGroup="entry" ControlToValidate="txtExpireDate" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" MaxLength="300" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSignature" runat="server" Text="Signature (Max 15 KB)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadAsyncUpload ID="uplSignatureImage" runat="server"
                                OnClientFilesUploaded="OnClientFilesUploaded"
                                OnFileUploaded="uplSignatureImage_FileUploaded"
                                MaxFileSize="15500" AllowedFileExtensions="jpeg,jpg,png,gif,bmp"
                                AutoAddFileInputs="false" Localization-Select="Upload" HideFileInput="True">
                            </telerik:RadAsyncUpload>
                            <telerik:RadBinaryImage ID="imgSignatureImage" runat="server" AlternateText=""
                                Width="100%"
                                Height="100%"
                                ResizeMode="Fill" BorderStyle="Double"></telerik:RadBinaryImage>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsLocked" runat="server" Text="Locked" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="User Group" PageViewID="pgUserGroup" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgServiceUnit">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView runat="server" ID="pgUserGroup" Selected="True">
            <telerik:RadGrid ID="grdUserUserGroup" runat="server" AutoGenerateColumns="false"
                AllowMultiRowSelection="true" OnNeedDataSource="grdUserUserGroup_NeedDataSource">
                <MasterTableView DataKeyNames="UserGroupID,IsSelect">
                    <Columns>
                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="50px" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="UserGroupID" HeaderText="Group ID"
                            UniqueName="UserGroupID" SortExpression="UserGroupID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="UserGroupName" HeaderText="Group Name" UniqueName="UserGroupName"
                            SortExpression="UserGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True"></Selecting>
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgServiceUnit">
            <telerik:RadGrid ID="grdUserServiceUnit" runat="server" AutoGenerateColumns="false"
                AllowMultiRowSelection="true" OnNeedDataSource="grdUserServiceUnit_NeedDataSource">
                <MasterTableView DataKeyNames="ServiceUnitID,IsSelect">
                    <Columns>
                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn2" HeaderStyle-Width="50px" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True"></Selecting>
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //<![CDATA[
            serverID("ajaxManagerID", "<%= AjaxManager.ClientID %>");
        //]]>
        </script>

    </telerik:RadCodeBlock>
</asp:Content>
