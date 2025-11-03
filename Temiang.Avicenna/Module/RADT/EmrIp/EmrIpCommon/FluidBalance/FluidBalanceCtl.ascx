<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FluidBalanceCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.FluidBalanceCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadAjaxPanel ID="ajaxPanel" runat="server">

    <telerik:RadTextBox runat="server" ID="txtRegistrationNo" Display="False" />
    <telerik:RadTextBox runat="server" ID="txtSequenceNo" Display="False" />

    <table style="width=100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align:top;" >
                <telerik:RadGrid ID="grdSchemaInfus" Width="98%" runat="server"
                    AutoGenerateColumns="False" GridLines="None"
                    AllowMultiRowEdit="false">
                    <MasterTableView DataKeyNames="SchemaInfusNo" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn DataField="SchemaInfusName" HeaderText="Schema Infus" UniqueName="SchemaInfusName"
                                HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn DataField="QtyVolume" HeaderText="Volume" UniqueName="Volume"
                                HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DecimalDigits="0" />
                            <telerik:GridNumericColumn DataField="QtyPerHour" HeaderText="CC / Hour" UniqueName="QtyPerHour"
                                HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DecimalDigits="0" />
                            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                        </Columns>

                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="false" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>

            </td>
            <td style="vertical-align:top;">
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr runat="server" id="trSchemaInfusOldVersion">
                        <td class="label" style="width: 80px">Schema Infus</td>
                        <td colspan="25">
                            <telerik:RadTextBox runat="server" ID="txtFluidBalSchemaInfus" ReadOnly="True" Width="100%"></telerik:RadTextBox></td>
                        <td style="width: 4px"></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 50px">Date</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtFluidBalInOutDate" Width="100px" ReadOnly="True"></telerik:RadTextBox></td>
                        <td style="width: 4px"></td>
                        <td class="label" style="width: 50px">Seq No</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtFluidBalSeqNo" NumberFormat-DecimalDigits="0" Width="40px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td style="width: 4px"></td>
                        <td class="label" style="width: 50px">Body Weight</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtBodyWeight" NumberFormat-DecimalDigits="2" Width="60px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td style="width: 4px"></td>
                        <td class="label" style="width: 50px">Age</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtAge" Width="80px" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>

                    </tr>
                </table>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="label" style="width: 50px">Total In</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtFluidBalQtyIn" NumberFormat-DecimalDigits="2" Width="60px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td style="width: 4px"></td>
                        <td class="label" style="width: 50px">Total Out</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtFluidBalQtyOut" NumberFormat-DecimalDigits="2" Width="60px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td style="width: 4px"></td>
                        <td class="label" style="width: 40px">IWL</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtFluidBalIwlQty" NumberFormat-DecimalDigits="2" Width="60px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td style="width: 4px"></td>


                        <td class="label" style="width: 50px">Balance</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtFluidBalBalanceQty" NumberFormat-DecimalDigits="2" Width="60px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td class="label" style="width: 40px">Diuresis</td>
                        <td>
                            <telerik:RadNumericTextBox runat="server" ID="txtDiuresis" NumberFormat-DecimalDigits="2" Width="60px" ReadOnly="True"></telerik:RadNumericTextBox></td>
                        <td style="width: 4px"></td>
                        <td></td>
                    </tr>
                </table>


            </td>
        </tr>
    </table>

    <div style="height:4px;width:100%;"></div>

    <telerik:RadGrid ID="grdFluidBalance" runat="server" OnNeedDataSource="grdFluidBalance_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdFluidBalance_DeleteCommand" Height="490px"
        OnItemCommand="grdFluidBalance_ItemCommand" OnItemDataBound="grdFluidBalance_OnItemDataBound">
        <MasterTableView DataKeyNames="RegistrationNo,SequenceNo,DetailSequenceNo" ShowGroupFooter="True" ShowFooter="True">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Fluid In" Name="FluidIn" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Fluid Out" Name="FluidOut" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Infus" Name="Infus" HeaderStyle-HorizontalAlign="Center" ParentGroupName="FluidIn">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Drug" Name="Drug" HeaderStyle-HorizontalAlign="Center" ParentGroupName="FluidIn">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Oral" Name="Oral" HeaderStyle-HorizontalAlign="Center" ParentGroupName="FluidIn">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Other" Name="OtherIn" HeaderStyle-HorizontalAlign="Center" ParentGroupName="FluidIn">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="colEdit" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%#  ScriptMonitoringEdit(Container)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Time" HeaderText="Time" HeaderStyle-Width="110px">
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "InOutDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="InfusName" UniqueName="InfusName" HeaderText="Infus" ColumnGroupName="Infus"
                    HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="InfusQtyIn" UniqueName="InfusQtyIn" HeaderText="Qty" ColumnGroupName="Infus"
                    HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SchemaInfusBalance" UniqueName="SchemaInfusBalance" HeaderText="Balance" ColumnGroupName="Infus"
                    HeaderStyle-Width="60px" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridBoundColumn>


                <telerik:GridBoundColumn DataField="DrugName" UniqueName="DrugName" HeaderText="Drug" ColumnGroupName="Drug"
                    HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="DrugQtyIn" UniqueName="DrugQtyIn" HeaderText="Qty" ColumnGroupName="Drug"
                    HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="OralName" UniqueName="OralName" HeaderText="Oral" ColumnGroupName="Oral"
                    HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="OralQtyIn" UniqueName="OralQtyIn" HeaderText="Qty" ColumnGroupName="Oral"
                    HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="OtherInName" UniqueName="OtherInName" HeaderText="Other" ColumnGroupName="OtherIn"
                    HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="OtherInQtyIn" UniqueName="OtherInQtyIn" HeaderText="Qty" ColumnGroupName="OtherIn"
                    HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridBoundColumn>

                <telerik:GridNumericColumn DataField="CummulativeQtyIn" UniqueName="CummulativeQtyIn" HeaderText="Cummulative" ColumnGroupName="FluidIn"
                    HeaderStyle-Width="85px" DataFormatString="{0:N2}">
                    <ItemStyle BackColor="#FAFAFA" Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>

                <telerik:GridNumericColumn DataField="UrineQty" UniqueName="UrineQty" HeaderText="Urine" ColumnGroupName="FluidOut" HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DefecateQty" UniqueName="DefecateQty" HeaderText="Defecate" ColumnGroupName="FluidOut" HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>

                <telerik:GridNumericColumn DataField="GagQty" UniqueName="GagQty" HeaderText="Gag / NGT" ColumnGroupName="FluidOut" HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>

                <telerik:GridNumericColumn DataField="BleedingQty" UniqueName="BleedingQty" HeaderText="Bleeding" ColumnGroupName="FluidOut" HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DrainQty" UniqueName="DrainQty" HeaderText="Drain / WSD CAPD" ColumnGroupName="FluidOut" HeaderStyle-Width="60px" Aggregate="Sum" DataFormatString="{0:N2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="CummulativeQtyOut" UniqueName="CummulativeQtyOut" HeaderText="Cummulative" ColumnGroupName="FluidOut"
                    HeaderStyle-Width="85px" DataFormatString="{0:N2}">
                    <ItemStyle BackColor="#FAFAFA" Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="FluidOutDescription" UniqueName="FluidOutDescription" HeaderText="Description" ColumnGroupName="FluidOut" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="UserName" UniqueName="UserName" HeaderText="PPA" HeaderStyle-Width="100px" />

                <telerik:GridTemplateColumn UniqueName="delcol" HeaderText="">
                    <ItemTemplate>
                        <%# (!IsMonitoringDeleteable(Container)  
                                ? string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />",Helper.UrlRoot()) : "")%>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                            Visible='<%#IsMonitoringDeleteable(Container) %>'
                            OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                            <img style="border: 0px; vertical-align: middle;" src="<%#Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>

            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldAlias="InOutDate" FieldName="InOutDate" HeaderText="Shift" FormatString="{0:dd/MM/yyyy}"></telerik:GridGroupByField>
                        <telerik:GridGroupByField FieldAlias="TimeGroup" FieldName="TimeGroup" HeaderText=" "></telerik:GridGroupByField>
                    </SelectFields>

                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="InOutDate" SortOrder="Ascending"></telerik:GridGroupByField>
                        <telerik:GridGroupByField FieldName="TimeGroup" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</telerik:RadAjaxPanel>
