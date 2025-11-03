<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="QualityIndicatorSurveyList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.QualityIndicatorSurveyList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(srid, surid) {
                var url = '';
                //alert(url = 'QISurvey_RI.aspx?md=view&srid=' + srid + '&surid=' + surid + '&prd=' + prd);
                //switch (srid) {
                //    case "IndikatorMutuRI":
                //        url = 'QISurvey_RI.aspx?md=view&srid=' + srid + '&surid=' + surid;
                //        break;
                //}
                url = 'QISurvey_RI.aspx?md=view&srid=' + srid + '&surid=' + surid;
                window.location.href = url;
            }

            function gotoAddUrl(srid) {
                var url = '';
                //switch (srid) {
                //    case "IndikatorMutuRI":
                //        url = 'QISurvey_RI.aspx?md=new&srid=' + srid;
                //        break;
                //}
                url = 'QISurvey_RI.aspx?md=new&srid=' + srid;
                window.location.href = url;
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" 
        GridLines="None" AutoGenerateColumns="false" AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="StandardReferenceID">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserAddAble.Equals(false) ? string.Empty : 
                            string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                  DataBinder.Eval(Container.DataItem, "StandardReferenceID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="StandardReferenceID"
                    HeaderText="Quality Indicator ID" UniqueName="StandardReferenceID" SortExpression="StandardReferenceID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StandardReferenceName" HeaderText="Quality Indicator Name"
                    UniqueName="StandardReferenceName" SortExpression="StandardReferenceName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="SurveyID" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                                                              DataBinder.Eval(Container.DataItem, "StandardReferenceID"), DataBinder.Eval(Container.DataItem, "SurveyID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="SurveyID" HeaderText="Survey ID"
                            UniqueName="SurveyID" SortExpression="SurveyID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PeriodDate" HeaderText="Period Date" 
                            UniqueName="PeriodDate" SortExpression="PeriodDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                        UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                       </telerik:GridBoundColumn>      
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApprove" HeaderText="Approved"
                            UniqueName="IsApprove" SortExpression="IsApprove" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false"/>
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>