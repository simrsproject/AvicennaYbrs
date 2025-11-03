<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="DietPatientsList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.DietPatientsList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(id, regno) {
                var uid = $find("<%= cboServiceUnitID.ClientID %>");
                var url = 'DietPatientsDetail.aspx?md=view&id=' + id + '&regno=' + regno + '&uid=' + uid.get_value();
                window.location.href = url;
            }

            function gotoAddUrl(regno, uid) {
                var url = 'DietPatientsDetail.aspx?md=new&regno=' + regno + '&uid=' + uid;
                window.location.href = url;
            }

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="group" HeaderText="Service Unit "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="group" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserAddAble.Equals(false) ? string.Empty : 
                            string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                    SortExpression="RegistrationNo" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                    UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Patient Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblPatientName" runat="server" Text='<%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName")) %>' /><br />
                        <i>Age : </i>
                        <asp:Label ID="lblAge" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Age") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Group" HeaderText="Service Unit" UniqueName="Group"
                    SortExpression="Group" HeaderStyle-Width="150px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    HeaderStyle-Width="80px" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FormOfFoodName" HeaderText="Form Of Food"
                    UniqueName="FormOfFoodName" SortExpression="FormOfFoodName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DietName" HeaderText="Diet Name" UniqueName="DietName"
                    SortExpression="DietName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DietCompName" HeaderText="Diet Comp" UniqueName="DietCompName"
                    SortExpression="DietCompName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="32px">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="TransactionNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                                                                                                DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="EffectiveStartDate" HeaderText="Effective Start Date"
                            UniqueName="EffectiveStartDate" SortExpression="EffectiveStartDate" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EffectiveEndDate" HeaderText="Effective End Date"
                            UniqueName="EffectiveEndDate" SortExpression="EffectiveEndDate" DataType="System.DateTime"
                            DataFormatString="{0:dd/MMM/yyyy HH:mm}" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FormOfFoodName" HeaderText="Form Of Food"
                            UniqueName="FormOfFoodName" SortExpression="FormOfFoodName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Diets" HeaderText="Diet" UniqueName="Diets" SortExpression="Diets"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DietComps" HeaderText="Diet Comp" UniqueName="DietComps"
                            SortExpression="DietComps" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Diagnose" HeaderText="Diagnose"
                            UniqueName="Diagnose" SortExpression="Diagnose" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Height" HeaderText="Height"
                            UniqueName="Height" SortExpression="Height" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Weight" HeaderText="Weight"
                            UniqueName="Weight" SortExpression="Weight" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="BodyMassIndex" HeaderText="Mass Index"
                            UniqueName="BodyMassIndex" SortExpression="BodyMassIndex" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="LastUpdateByUserID"
                            HeaderText="Updated By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detailItem" DataKeyNames="TransactionNo, DietID" AutoGenerateColumns="false"
                            GroupLoadMode="Client">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="DietID" HeaderText="Diet ID"
                                    UniqueName="DietID" SortExpression="DietID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="DietName" HeaderText="Diet Name" UniqueName="DietName"
                                    SortExpression="DietName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="DietComps" HeaderText="Diet Comp" UniqueName="DietComps"
                                    SortExpression="DietComps" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="MenuName" HeaderText="Menu" UniqueName="MenuName"
                                    SortExpression="MenuName" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FormOfFoodName" HeaderText="Form of Food"
                                    UniqueName="FormOfFoodName" SortExpression="FormOfFoodName" Visible="False" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Calorie" HeaderText="Calorie"
                                    UniqueName="Calorie" SortExpression="Calorie" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Protein" HeaderText="Protein"
                                    UniqueName="Protein" SortExpression="Protein" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Fat" HeaderText="Fat"
                                    UniqueName="Fat" SortExpression="Fat" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="90px" DataField="Carbohydrate" HeaderText="Carbohydrate"
                                    UniqueName="Carbohydrate" SortExpression="Carbohydrate" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Salt" HeaderText="Salt"
                                    UniqueName="Salt" SortExpression="Salt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Fiber" HeaderText="Fiber"
                                    UniqueName="Fiber" SortExpression="Fiber" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
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
