<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeProcedureCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.EpisodeProcedureCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<telerik:RadAjaxManagerProxy ID="ajxProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdProcedure">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdProcedure" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function onProcedureNameClick(value) {
            var grid = $find("<%=grdProcedure.ClientID%>");
            var txt = $telerik.findElement(grid.get_element(), "txtProcedureName");
            txt.value = value;
        }

    </script>
</telerik:RadCodeBlock>
<telerik:RadGrid ID="grdProcedure" Width="100%" GridLines="None" runat="server"
    AllowAutomaticInserts="True" PageSize="10"
    OnInsertCommand="grdProcedure_OnInsertCommand"
    OnUpdateCommand="grdProcedure_OnUpdateCommand"
    OnDeleteCommand="grdProcedure_OnDeleteCommand"
    OnNeedDataSource="grdProcedure_OnNeedDataSource"
    OnItemCommand="grdProcedure_OnItemCommand"
    OnItemDataBound="grdProcedure_ItemDataBound"
    AllowAutomaticUpdates="False"
    AutoGenerateColumns="False"
    AllowMultiRowEdit="False"
    AllowAutomaticDeletes="False">

    <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="SequenceNo"
        HorizontalAlign="NotSet" AutoGenerateColumns="False">
        <CommandItemSettings AddNewRecordImageUrl="~/Images/Toolbar/insert16.png" AddNewRecordText="Add ICD 9"></CommandItemSettings>
        <SortExpressions>
            <telerik:GridSortExpression FieldName="SequenceNo" SortOrder="Ascending" />
        </SortExpressions>
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Toolbar/edit16.png" HeaderStyle-Width="30px"></telerik:GridEditCommandColumn>
            <telerik:GridBoundColumn HeaderText="ID" HeaderStyle-Width="80px" UniqueName="ProcedureID" DataField="ProcedureID" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
            <telerik:GridTemplateColumn HeaderText="Procedure" UniqueName="ProcedureName" DataField="ProcedureName">
                <ItemTemplate>
                    <%# Eval("ProcedureName") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Synonym" UniqueName="ProcedureSynonym" DataField="ProcedureSynonym">
                <ItemTemplate>
                    <%# Eval("ProcedureSynonym") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn HeaderText="Date" HeaderStyle-Width="80px" UniqueName="ProcedureDate" DataField="ProcedureDate" />
            <telerik:GridBoundColumn HeaderText="Time" HeaderStyle-Width="80px" UniqueName="ProcedureTime" DataField="ProcedureTime" />
            <telerik:GridCheckBoxColumn HeaderText="Void" HeaderStyle-Width="40px" UniqueName="IsVoid" DataField="IsVoid" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
            <telerik:GridTemplateColumn HeaderText="" UniqueName="colVoidUnVoid" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Delete" OnClientClick="javascript:if (!confirm('Continue ?')) return false;"><%# Eval("IsVoid").ToBoolean()?"UnVoid":"Void" %></asp:LinkButton>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
        <EditFormSettings EditFormType="Template">
            <FormTemplate>
                <table>
                    <tr>
                        <td class="label" style="width: 100px">Procedure ID</td>
                        <td>
                            <telerik:RadComboBox ID="cboProcedureID" runat="server" Width="300px" EmptyMessage="Select a Procedure" Text='<%# Bind("ProcedureID") %>'
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true" OnSelectedIndexChanged="cboProcedureID_SelectedIndexChanged">
                                <WebServiceSettings Method="Procedures" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                                                    <div onclick="onProcedureNameClick('#= Attributes.ProcedureName #')">
                                                        <ul class="details">
                                                            <li class="bold"><span>#= Value #</span></li>
                                                            <li class="small"><span>#= Attributes.ProcedureName #</span></li>
                                                        </ul>
                                                    </div>
                                </ClientItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 100px">Procedure Name</td>
                        <td>
                            <telerik:RadTextBox ID="txtProcedureName" runat="server" Width="300px" MaxLength="250" Text='<%# Bind("ProcedureName") %>'
                                TextMode="MultiLine" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProcedureSynonym" runat="server" Text="Synonym"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboProcedureSynonym" runat="server" Width="300px" EmptyMessage="Select a Synonym" Text='<%# Bind("ProcedureSynonym") %>'>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 100px">Date Time</td>
                        <td>
                            <telerik:RadDatePicker ID="txtProcedureDate" runat="server" Width="100px"  />
                            <telerik:RadMaskedTextBox ID="txtProcedureTime" runat="server" Mask="<00..23>:<00..59>"
                                PromptChar="_" RoundNumericRanges="false" Width="50px" Text='<%# Bind("ProcedureTime")%>'>
                            </telerik:RadMaskedTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                CommandName="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </FormTemplate>
        </EditFormSettings>
    </MasterTableView>
</telerik:RadGrid>

