<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicationHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.Medication.MedicationHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadGrid ID="grdMedicationHist" runat="server" Width="2000px"
    AutoGenerateColumns="False" GridLines="None" AllowPaging="False">
    <MasterTableView>
        <Columns>
            <telerik:GridTemplateColumn UniqueName="ItemDescription" HeaderStyle-Width="300px" HeaderText="Item" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <%# Temiang.Avicenna.BusinessObject.MedicationReceive.PrescriptionItemDescription(DataBinder.Eval( Container.DataItem, "RefTransactionNo"), DataBinder.Eval( Container.DataItem, "RefSequenceNo"), DataBinder.Eval( Container.DataItem, "ItemDescription"),false,DataBinder.Eval( Container.DataItem, "SRMedicationRoute"))%><br />
                    <br />
                    Start Time: <%# DataBinder.Eval(Container.DataItem, "StartDateTime") == DBNull.Value? String.Empty: Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "StartDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute)%>
                    <%# MedicationEditHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),PatientID) %>
                    <br />
                    <%# MedicationChangeConsumeMethodHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "BalanceQty"), 
    DataBinder.Eval(Container.DataItem, "IsAntibiotic"), DataBinder.Eval(Container.DataItem, "IsVoid"), DataBinder.Eval(Container.DataItem, "IsContinue")) %>

                    <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQtyInString")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>&nbsp;
                    
                    <%# DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName")==DBNull.Value?string.Empty:String.Format("{0}<br,>",DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName"))%><br />
                    Schedule: <%# MedicationScheduleSetupHtml(Container)%>)<br />
                    <%# MedicationStopContinueHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "IsVoid"),DataBinder.Eval(Container.DataItem, "IsContinue"),DataBinder.Eval(Container.DataItem, "PatientID")) %>&nbsp;&nbsp;
                    <%# MedicationVoidHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "IsVoid"),DataBinder.Eval(Container.DataItem, "PatientID")) %>&nbsp;&nbsp;
                    <%--Presc No:&nbsp;<a style="cursor: pointer;" onclick="showPrescription('<%#DataBinder.Eval(Container.DataItem, "RefTransactionNo") %>')"><img src="<%#Helper.UrlRoot() %>/Images/Toolbar/views16.png" />&nbsp;<%#DataBinder.Eval(Container.DataItem, "RefTransactionNo") %></a>--%>
                    <%#!0.Equals(Eval("RasproSeqNo"))? string.Format("&nbsp;&nbsp;<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"{4}/Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"Raspro Form\" />&nbsp;{3}</a>",
                                            PatientID,RegistrationNo,Eval("RasproSeqNo"),Eval("SRRaspro"),Helper.UrlRoot()):string.Empty %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Qty" HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Top" Visible="false" />
            <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Bal. (Setup)" HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Top" />
            <telerik:GridNumericColumn DataField="BalanceRealQty" UniqueName="BalanceRealQty" HeaderText="Bal. (Real)" HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Top" />
            <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Top" />

            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="70px" HeaderText="">
                <ItemTemplate>
                    <table id='medused'>
                        <tr>
                            <th colspan="5">Date</th>
                        </tr>
                        <tr>
                            <td>Sched</td>
                        </tr>
                        <tr>
                            <td>Hov</td>
                        </tr>
                        <tr>
                            <td>Real</td>
                        </tr>
                        <tr>
                            <td>Bal</td>
                        </tr>
                        <tr>
                            <td>Setup By</td>
                        </tr>
                        <tr>
                            <td>Hov By</td>
                        </tr>
                        <tr>
                            <td>Hov To</td>
                        </tr>
                        <tr>
                            <td>Verif By</td>
                        </tr>
                        <tr>
                            <td>Real By</td>
                        </tr>
                        <tr>
                            <td>Consume</td>
                        </tr>
                        <tr>
                            <td>Serv Unt</td>
                        </tr>
                        <tr>
                            <td>Note</td>
                        </tr>
                        <tr>
                            <td>Sign Ptn</td>
                        </tr>
                    </table>

                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="01" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 1)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="02" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 2)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="03" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 3)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="04" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 4)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="05" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 5)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="06" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 6)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="07" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 7)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="08" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 8)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="09" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 9)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="220px" HeaderText="10" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(Container, 10)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="">
                <ItemTemplate>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"  FrozenColumnsCount="6"></Scrolling>
    </ClientSettings>
</telerik:RadGrid>