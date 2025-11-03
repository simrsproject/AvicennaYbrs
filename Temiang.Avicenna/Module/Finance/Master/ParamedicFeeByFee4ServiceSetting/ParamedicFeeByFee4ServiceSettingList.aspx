<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeByFee4ServiceSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeByFee4ServiceSettingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDataBound="grdList_DataBound"
                OnPreRender="grdList_PreRender" >
                <MasterTableView DataKeyNames="Id">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ParamedicStatusName" HeaderText="Paramedic Type" UniqueName="ParamedicStatusName"
                            SortExpression="ParamedicStatusName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SpecialtyName" HeaderText="Specialty" UniqueName="SpecialtyName"
                            SortExpression="SpecialtyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="RegistrationTypeName" HeaderText="Registration Type" UniqueName="RegistrationTypeName"
                            SortExpression="RegistrationTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="TariffTypeName" HeaderText="Tarif Name" UniqueName="TariffTypeName"
                            SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class" UniqueName="ClassName"
                            SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="GuarantorTypeName" HeaderText="Guarantor Type Name" UniqueName="GuarantorTypeName"
                            SortExpression="GuarantorTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group" UniqueName="ItemGroupName"
                            SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                            SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tarif Component Name" UniqueName="TariffComponentName"
                            SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn DataField="IsFeeValueFromPlafon" HeaderText="Is From Plafon" UniqueName="IsFeeValueFromPlafon"
                            SortExpression="IsFeeValueFromPlafon" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="IsFeeValueFromTariffPrice" HeaderText="Is From Tariff" UniqueName="IsFeeValueFromTariffPrice"
                            SortExpression="IsFeeValueFromTariffPrice" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="IsFeeValueInPercent" HeaderText="Is Value In Percent" UniqueName="IsFeeValueInPercent"
                            SortExpression="IsFeeValueInPercent" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridCheckBoxColumn>

                        <telerik:GridBoundColumn DataField="FeeValue" HeaderText="Fee Value" UniqueName="FeeValue" 
                            SortExpression="FeeValue" DataFormatString="{0:n2}" >
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unit" HeaderText="" UniqueName="unit"
                            SortExpression="unit">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="Formula" HeaderText="Formula" UniqueName="Formula"
                            SortExpression="Formula" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
    
</asp:Content>
