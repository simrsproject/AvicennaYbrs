<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParticipantItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.ParticipantItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="TransPrescriptionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransPrescriptionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="33%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">Employee ID</td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEmployeeID" runat="server" Width="300px" EnableLoadOnDemand="true" AutoPostBack="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                            OnItemsRequested="cboEmployeeID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ErrorMessage="Employee ID required."
                            ValidationGroup="TransPrescriptionItem" ControlToValidate="cboEmployeeID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" Enabled="false" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransPrescriptionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="TransChargesItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="34%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">Evaluator ID</td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEvaluatorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                            OnItemsRequested="cboEmployeeID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td class="label">Evaluator Type</td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREvaluatorType" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:Button ID="btnAddEvaluator" runat="server" Text="Add" OnClick="btnAddEvaluator_Click" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadGrid ID="grdParticipantEvaluator" runat="server" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="grdParticipantEvaluator_NeedDataSource" OnDeleteCommand="grdParticipantEvaluator_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ParticipantEvaluatorID, ParticipantItemID, EvaluatorID, SREvaluatorType">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Evaluator Name"
                                        UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Evaluator Type" HeaderStyle-Width="100px"
                                        UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="false">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
        </td>
        <td width="33%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">Questionnaire ID</td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboQuestionerID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboQuestionerID_ItemDataBound"
                            OnItemsRequested="cboQuestionerID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "QuestionerNo") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "QuestionerName")%>)</b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Evaluator ID</td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEvaluatorQuestionerID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                            OnItemsRequested="cboEmployeeQuestionerID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:Button ID="btnAddQuestioner" runat="server" Text="Add" OnClick="btnAddQuestioner_Click" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadGrid ID="grdQuestionerItem" runat="server" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="grdQuestionerItem_NeedDataSource" OnDeleteCommand="grdQuestionerItem_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ParticipantQuestionerID, ParticipantItemID, QuestionerID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="QuestionerName" HeaderText="Questionnaire Name"
                                        UniqueName="QuestionerName" SortExpression="QuestionerName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="QuestionerEvaluatorName" HeaderText="Evaluator Name"
                                        UniqueName="QuestionerEvaluatorName" SortExpression="QuestionerEvaluatorName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="false">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
