<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ReferralDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.Referralv2.ReferralDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            var txtPCareItemID = "";
            function openWinPCareLookUp(sender, eventArgs) {
                txtPCareItemID = sender._clientID;
                var oWnd = $find("<%= winPCareLookUp.ClientID %>");
                var url = '<%=Page.ResolveUrl("~/PCareCommon/LookUp/LookUpMaster.aspx")+"?rtype=Provider"%>';
                // JANGAN PAKAI radopen,  urlnya harus lengkap dgn rootnya jika pakai radopen
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();
            }

            function onWinPCareLookUpClose(oWnd, args) {
                var txtCode = $find(txtPCareItemID);

                if (oWnd.argument)
                    txtCode.set_value(oWnd.argument.code);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winPCareLookUp" Animation="None" Width="600px" Height="400px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onWinPCareLookUpClose">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Referral Group ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Referral Group ID required."
                                ValidationGroup="entry" ControlToValidate="txtItemID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Referral Group Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="100" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Referral Group Name required."
                                ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceID" runat="server" Text="RL Report Reference"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboReferenceID" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="80px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdReferral" runat="server" AllowMultiRowSelection="true"
                    OnNeedDataSource="grdReferral_NeedDataSource" AutoGenerateColumns="False"
                    GridLines="None" OnUpdateCommand="grdReferral_UpdateCommand" OnDeleteCommand="grdReferral_DeleteCommand"
                    OnInsertCommand="grdReferral_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ReferralID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ReferralID" HeaderText="Referral ID"
                                UniqueName="ReferralID" SortExpression="ReferralID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ReferralName" HeaderText="Referral Name" UniqueName="ReferralName"
                                SortExpression="ReferralName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="StreetName" HeaderText="Address"
                                UniqueName="StreetName" SortExpression="StreetName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="City" HeaderText="City"
                                UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PhoneNo" HeaderText="Phone No"
                                UniqueName="PhoneNo" SortExpression="PhoneNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="MobilePhoneNo" HeaderText="Mobile Phone No"
                                UniqueName="MobilePhoneNo" SortExpression="MobilePhoneNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Email" HeaderText="Email"
                                UniqueName="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsRefferalFrom" HeaderText="Referral From"
                                UniqueName="IsRefferalFrom" SortExpression="IsRefferalFrom" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRefferalTo" HeaderText="Referral To"
                                UniqueName="IsRefferalTo" SortExpression="IsRefferalTo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText=" Active"
                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ReferralDetailItem.ascx" EditFormType="WebUserControl">
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
