<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="RegistrationInfoMedicEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.RegistrationInfoMedicEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInputType" runat="server" Text="Input Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfTmpID" runat="server" />
                            <telerik:RadComboBox ID="cboInputType" runat="server" AutoPostBack="true" Width="304px"
                                OnSelectedIndexChanged="cboInputType_OnSelectedIndexChanged" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDateTimePicker ID="dateInfo" runat="server" AutoPostBackControl="None">
                            </telerik:RadDateTimePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nursing implementation date time required."
                                ValidationGroup="RegInfoValGroup" ControlToValidate="dateInfo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInfo1" runat="server" Text="Info 1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo1" runat="server" Width="304px" Height="80px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="row2">
                        <td class="label">
                            <asp:Label ID="lblInfo2" runat="server" Text="Info 2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo2" runat="server" Width="304px" Height="80px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="row3">
                        <td class="label">
                            <asp:Label ID="lblInfo3" runat="server" Text="Info 3"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo3" runat="server" Width="304px" Height="80px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="row4">
                        <td class="label">
                            <asp:Label ID="lblInfo4" runat="server" Text="Info 4"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo4" runat="server" Width="304px" Height="80px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>


            </td>
            <td width="260px" style="vertical-align: top;">
                <telerik:RadGrid ID="grdVitalSign" Width="260px" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    GridLines="None" OnNeedDataSource="grdVitalSign_NeedDataSource" OnItemDataBound="grdVitalSign_ItemDataBound">
                    <MasterTableView DataKeyNames="VitalSignID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="VitalSignID" Display="False" UniqueName="VitalSignID" />
                            <telerik:GridBoundColumn DataField="VitalSignUnit" Display="False" UniqueName="VitalSignUnit" />
                            <telerik:GridBoundColumn DataField="ValueType" Display="False" UniqueName="ValueType" />
                            <telerik:GridBoundColumn DataField="NumDecimalDigits" Display="False" UniqueName="NumDecimalDigits" />
                            <telerik:GridBoundColumn DataField="NumMinValue" Display="False" UniqueName="NumMinValue" />
                            <telerik:GridBoundColumn DataField="NumMaxValue" Display="False" UniqueName="NumMaxValue" />
                            <telerik:GridBoundColumn DataField="NumMaxLength" Display="False" UniqueName="NumMaxLength" />
                            <telerik:GridBoundColumn DataField="VitalSignValueText" Display="False" UniqueName="VitalSignValueText" />
                            <telerik:GridBoundColumn DataField="VitalSignValueNum" Display="False" UniqueName="VitalSignValueNum" />
                            <telerik:GridTemplateColumn HeaderText="Vital Sign" UniqueName="VitalSignValue">
                                <ItemTemplate>
                                    <telerik:RadMaskedTextBox Visible='<%# "TXT".Equals(DataBinder.Eval(Container.DataItem, "ValueType")) %>'
                                        ID="txtValueType_TXT" runat="server"
                                        SelectionOnFocus="SelectAll"
                                        Label='<%# DataBinder.Eval(Container.DataItem, "VitalSignName") %>'
                                        LabelWidth="150px"
                                        Width="100px"
                                        Mask='<%# DataBinder.Eval(Container.DataItem, "EntryMask") %>'>
                                    </telerik:RadMaskedTextBox>
                                    <telerik:RadNumericTextBox Visible='<%# "NUM".Equals(DataBinder.Eval(Container.DataItem, "ValueType")) %>'
                                        ID="txtValueType_NUM" runat="server" SelectionOnFocus="SelectAll"
                                        Type='Number'
                                        Label='<%#string.Format("{0} ({1})", DataBinder.Eval(Container.DataItem, "VitalSignName"),DataBinder.Eval(Container.DataItem, "VitalSignUnit")) %>'
                                        LabelWidth="150px"
                                        Width="100px">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                        <Resizing AllowColumnResize="False" />
                    </ClientSettings>
                </telerik:RadGrid>

            </td>
        </tr>
    </table>


</asp:Content>
