<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="MarginList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.MarginList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="MarginID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MarginID" HeaderText="Margin ID"
                    UniqueName="MarginID" SortExpression="MarginID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="MarginName" HeaderText="Margin Name" UniqueName="MarginName"
                    SortExpression="MarginName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="MarginID" Name="grdReferenceItem" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SequenceNo" HeaderText="No"
                            UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="StartingValue" HeaderText="Starting Value"
                            UniqueName="StartingValue" SortExpression="StartingValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="EndingValue" HeaderText="Ending Value"
                            UniqueName="EndingValue" SortExpression="EndingValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MarginPercentage"
                            HeaderText="Global (%)" UniqueName="MarginPercentage" SortExpression="MarginPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsGlobalWithoutVAT" HeaderText="-VAT"
                            UniqueName="IsGlobalWithoutVAT" SortExpression="IsGlobalWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="InpatientMarginPercentage"
                            HeaderText="Inpatient (%)" UniqueName="InpatientMarginPercentage" SortExpression="InpatientMarginPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsIpWithoutVAT" HeaderText="-VAT"
                            UniqueName="IsIpWithoutVAT" SortExpression="IsIpWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OutpatientMarginPercentage"
                            HeaderText="Outpatient (%)" UniqueName="OutpatientMarginPercentage" SortExpression="OutpatientMarginPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsOpWithoutVAT" HeaderText="-VAT"
                            UniqueName="IsOpWithoutVAT" SortExpression="IsOpWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmergencyMarginPercentage"
                            HeaderText="Emergency (%)" UniqueName="EmergencyMarginPercentage" SortExpression="EmergencyMarginPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsEmWithoutVAT" HeaderText="-VAT"
                            UniqueName="IsEmWithoutVAT" SortExpression="IsEmWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OTCMarginPercentage"
                            HeaderText="OTC (%)" UniqueName="OTCMarginPercentage" SortExpression="OTCMarginPercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsOtcWithoutVAT" HeaderText="-VAT"
                            UniqueName="IsOtcWithoutVAT" SortExpression="IsOtcWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn DataField="IsMinusDiscount" HeaderText="Minus Discount"
                            UniqueName="IsMinusDiscount" SortExpression="IsMinusDiscount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="False" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
