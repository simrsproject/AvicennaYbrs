<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ParticipantDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.ParticipantDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Participant Name</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParticipantName" runat="server" Width="300px" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvParticipantName" runat="server" ErrorMessage="Participant Name required."
                                ValidationGroup="entry" ControlToValidate="txtParticipantName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Period Year</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" /></td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPeriodYear" runat="server" ErrorMessage="Year required."
                                ValidationGroup="entry" ControlToValidate="txtPeriodYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRQuarterPeriod" runat="server" Text="Quarter"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRQuarterPeriod" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeType" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Section / Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                           <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                            OnItemsRequested="cboServiceUnitID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr runat="server" id="trAppraisalType" visible="false">
                        <td class="label">Appraisal Type</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAppraisalType" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRAppraisalType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRAppraisalType" runat="server" ErrorMessage="Appraisal Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRAppraisalType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr runat="server" id="trScoringRecapitulation" visible="false">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsScoringRecapitulation" runat="server" Text="Scoring Recapitulation" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Notes</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    
                </table>
            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdParticipantItem" runat="server" OnNeedDataSource="grdParticipantItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdParticipantItem_UpdateCommand"
        OnDeleteCommand="grdParticipantItem_DeleteCommand" OnInsertCommand="grdParticipantItem_InsertCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ParticipantItemID, ParticipantID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Evaluators" HeaderText="Evaluators"
                    UniqueName="Evaluators" SortExpression="Evaluators" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Questioners" HeaderText="Questionnaire"
                    UniqueName="Questioners" SortExpression="Questioners" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ParticipantItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ParticipantItemDetailEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
