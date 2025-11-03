<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeByFee4ServiceSettingMultiLevelList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.V7.ParamedicFeeByFee4ServiceSettingMultiLevelList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoNewUrl(id) {
                var url = 'ParamedicFeeByFee4ServiceSettingMultiLevelDetail.aspx?md=new&id=' + id;
                window.location.href = url;
            }

            function gotoEditUrl(id) {
                var url = 'ParamedicFeeByFee4ServiceSettingMultiLevelDetail.aspx?md=edit&id=' + id;
                window.location.href = url;
            }

            function gotoViewUrl(id) {
                var url = 'ParamedicFeeByFee4ServiceSettingMultiLevelDetail.aspx?md=view&id=' + id
                window.location.href = url;
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Level 1" PageViewID="pg1"
                Selected="True">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pg1" runat="server" Selected="true">
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
                        
                        <telerik:GridBoundColumn DataField="RegistrationTypeMergeBillingName" HeaderText="Registration Type Merge Billing" UniqueName="RegistrationTypeMergeBillingName"
                            SortExpression="RegistrationTypeMergeBillingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

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

                        <telerik:GridBoundColumn DataField="Formula" HeaderText="Bruto" UniqueName="Formula"
                            SortExpression="Formula" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FormulaNetto" HeaderText="Netto" UniqueName="FormulaNetto"
                            SortExpression="FormulaNetto" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="cview" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"> <img src=\"../../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                                                  DataBinder.Eval(Container.DataItem, "Id")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="cedit" HeaderText="">
                            <ItemTemplate>
                                <%# (IsUserEditAble ? (string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}'); return false;\"> <img src=\"../../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                                                  DataBinder.Eval(Container.DataItem, "Id"))) : string.Empty)%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="cnew" HeaderText="New">
                            <ItemTemplate>
                                <%# (IsUserEditAble ? (string.Format("<a href=\"#\" onclick=\"gotoNewUrl('{0}'); return false;\"> <img src=\"../../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                                                  DataBinder.Eval(Container.DataItem, "Id"))) : string.Empty)%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
