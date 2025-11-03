<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PioEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.PioEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function showDropDown(sender, eventArgs) {
            sender.showDropDown();
            sender.requestItems("[showall]", false);
        }
    </script>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">No 
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPioNo" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date 
                        </td>
                        <td class="entry">
                            <telerik:RadDateTimePicker ID="txtPioDateTime" runat="server" Width="170px" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Time required."
                                ValidationGroup="entry" ControlToValidate="txtPioDateTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Question Method
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRQuestionMethod" runat="server" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Questioner Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestionerName" runat="server" Width="100%" MaxLength="150" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Questioner Name required."
                                ValidationGroup="entry" ControlToValidate="txtInformation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">From
                        </td>
                        <td class="entry">
                            <telerik:RadRadioButtonList runat="server" ID="optRecipentType" AutoPostBack="false">
                                <Items>
                                    <telerik:ButtonListItem Text="External Hospital" Value="0" />
                                    <telerik:ButtonListItem Text="Internal Hospital" Value="1" />
                                </Items>
                            </telerik:RadRadioButtonList>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Service Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="100%" EmptyMessage="Select a Service Unit"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" OnClientFocus="showDropDown">
                                <WebServiceSettings Method="ServiceUnitCares" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Status
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSROccupation" runat="server" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Question
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtQuestion" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="150" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Question required."
                                ValidationGroup="entry" ControlToValidate="txtInformation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" valign="top">Question Category
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadGrid ID="grdPioCategoryLine" Width="99%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                                AllowMultiRowSelection="True"
                                OnNeedDataSource="grdPioCategoryLine_NeedDataSource" OnItemDataBound="grdPioCategoryLine_ItemDataBound">
                                <MasterTableView DataKeyNames="ItemID,ReferenceID" ShowHeader="false" ShowHeadersWhenNoRecords="false" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkIsSelected" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                        <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Category" />
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                                    <Resizing AllowColumnResize="False" />
                                    <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                                </ClientSettings>
                            </telerik:RadGrid>

                            <table style="width: 100%">
                                <tr>
                                    <td class="label">Other Category
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtOtherCategory" runat="server" Width="100%" MaxLength="300" />
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Answer
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInformation" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="800" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Information required."
                                ValidationGroup="entry" ControlToValidate="txtInformation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" valign="top">Reference
                        </td>
                        <td colspan="2">
                            <telerik:RadGrid ID="grdPioSourceLine" Width="99%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                                AllowMultiRowSelection="True"
                                OnNeedDataSource="grdPioSourceLine_NeedDataSource" OnItemDataBound="grdPioSourceLine_ItemDataBound">
                                <MasterTableView DataKeyNames="ItemID,ReferenceID" ShowHeader="false" ShowHeadersWhenNoRecords="false" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkIsSelected" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                        <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Source" />
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                                    <Resizing AllowColumnResize="False" />
                                    <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                                </ClientSettings>
                            </telerik:RadGrid>
                            <table style="width: 100%">
                                <tr>
                                    <td class="label">Other Reference
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtOtherSource" runat="server" Width="100%" TextMode="MultiLine" MaxLength="300" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Answer Duration
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDurationType" runat="server" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Answer Date 
                        </td>
                        <td class="entry">
                            <telerik:RadDateTimePicker ID="txtAnswerDateTime" runat="server" Width="170px" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Answer Method
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAnswerMethod" runat="server" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
</asp:Content>
