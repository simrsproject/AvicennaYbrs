<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="NursingDiagnosaDetail.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingDiagnosaDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%;vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNursingDiagnosaID" runat="server" Width="100px" MaxLength="20" ReadOnly />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="ID required."
                                ValidationGroup="entry" ControlToValidate="txtNursingDiagnosaID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Sequence No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="20" ReadOnly />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence no required."
                                ValidationGroup="entry" ControlToValidate="txtSequenceNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Diagnosis Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDiagCode" runat="server" Width="100px" MaxLength="20" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNursingParent" runat="server" Text="Parent"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboNursingParent" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboNursingParent_ItemsRequested"
                                OnItemDataBound="cboNursingParent_ItemDataBound" OnSelectedIndexChanged="cboNursingParent_SelectedIndexChanged"
                                AutoPostBack="true">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvNursingParent" runat="server" ErrorMessage="Diagnosis parent required."
                                ValidationGroup="entry" ControlToValidate="cboNursingParent" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" Text="Diagnose Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboDiagType" runat="server" Width="300px" 
                                            OnSelectedIndexChanged="cboDiagType_SelectedIndexChanged" AutoPostBack="true" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfDiagType" runat="server" ErrorMessage="Diagnose type required."
                                            ValidationGroup="entry" ControlToValidate="cboDiagType" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNursingDiagnosaName" runat="server" Text="Diagnosis Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNursingDiagnosaName" runat="server" Width="300px" MaxLength="450" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvClassName" runat="server" ErrorMessage="Diagnosis Name required."
                                ValidationGroup="entry" ControlToValidate="txtNursingDiagnosaName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trDefinition" runat="server">
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Definition"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDefinition" runat="server" Width="300px" MaxLength="450" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr id="trNsEtiology" runat="server">
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" Text="Etiology Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboEtiologyType" runat="server" Width="300px" />
                                    </td>
                                    <td width="20">
                                        <asp:RequiredFieldValidator ID="rfEtiologyType" runat="server" ErrorMessage="Etiology type required."
                                            ValidationGroup="entry" ControlToValidate="cboEtiologyType" SetFocusOnError="True"
                                            Width="100%">
                                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trNocType" runat="server">
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Noc Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRNursingNocType" runat="server" Width="300px" AutoPostBack="true" />
                        </td>
                        <td width="20">

                        </td>
                    </tr>
                    <tr id="trNicType" runat="server">
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Nic Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRNursingNicType" runat="server" Width="300px" AutoPostBack="true" />
                        </td>
                        <td width="20">

                        </td>
                    </tr>

                    <tr id="trF1" runat="server">
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Group 1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtF1" runat="server" Width="300px" MaxLength="450" TextMode="MultiLine" />
                        </td>
                        <td width="20">

                        </td>
                    </tr>
                    <tr id="trF2" runat="server">
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Group 2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtF2" runat="server" Width="300px" MaxLength="450" TextMode="MultiLine" />
                        </td>
                        <td width="20">

                        </td>
                    </tr>

                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr id="trRespondTemplate" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Respond Template Text"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRespondTemplate" runat="server" Width="300px" MaxLength="50" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr id="trRespondTemplateID" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Respond Template"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboNursingDiagnosaTemplateID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboNursingDiagnosaTemplateID_ItemsRequested"
                                OnItemDataBound="cboNursingDiagnosaTemplateID_ItemDataBound"
                                AutoPostBack="true">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlNsType" runat="server">
                                <telerik:RadGrid ID="grdNsType" runat="server" 
                                    OnNeedDataSource="grdNsType_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None"
                                    OnItemDataBound="grdNsType_ItemDataBound">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SRNsTypeID">
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                        runat="server" /><br />
                                                    <span>&nbsp;</span>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRNsTypeID" HeaderText="Nursing Type"
                                                UniqueName="SRNsTypeID" SortExpression="SRNsTypeID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="SRNsTypeName" HeaderText="Type" UniqueName="NursingTypeName"
                                                SortExpression="SRNsTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <asp:Panel ID="pnlSU" runat="server">
                    <telerik:RadGrid ID="gridServiceUnit" runat="server" AutoGenerateColumns="false"
                        GridLines="None" OnNeedDataSource="gridServiceUnit_NeedDataSource" 
                        OnItemDataBound="gridServiceUnit_ItemDataBound">
                        <MasterTableView ClientDataKeyNames="ServiceUnitID" DataKeyNames="ServiceUnitID">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                            runat="server" /><br />
                                        <span>&nbsp;</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ServiceUnitID" HeaderText="Service Unit ID"
                                    SortExpression="ServiceUnitID" UniqueName="ServiceUnitID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name"
                                    SortExpression="ServiceUnitName" UniqueName="ServiceUnitName">
                                    <HeaderStyle HorizontalAlign="Left" Width="100%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
