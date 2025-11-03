<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="QueueingSoundList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.QueueingSoundList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1" >
        <script type="text/javascript">
            function gotoViewUrl(soid) {
                var url = 'QueueingSoundDetail.aspx?md=view&soid=' + soid + '';
                window.location.href = url;
            }

            function gotoEditUrl(soid) {
                var url = 'QueueingSoundDetail.aspx?md=edit&soid=' + soid + '';
                window.location.href = url;
            }
             
           function openWinAdd(soid) {
               var url = 'QueueingSoundDetail.aspx?md=new&soid=' + soid + '';
               window.location.href = url;
            }

            function gotoViewUrl1(serId) {
                var url = 'PoliSoundDetail.aspx?md=view&serId=' + serId + '';
                window.location.href = url;
            }

            function openWinProcess(serId) {
                var url = 'PoliSoundDetail.aspx?md=edit&serId=' + serId + '';
                window.location.href = url;
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPoli" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>


    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Master Audio" PageViewID="pgQueueing" Selected="True" />
            <telerik:RadTab runat="server" Text="Service Unit" PageViewID="pgPoli"  />
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgQueueing" runat="server" Selected="true">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" >
                <MasterTableView DataKeyNames="SoundID" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lbNew" runat="server" OnClientClick="javascript:openWinAdd('{0}');return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" /> &nbsp; <asp:Label
                                runat="server" ID="lblNew" Text="New"></asp:Label> 
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" >
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\"/></a>",
                                DataBinder.Eval(Container.DataItem, "SoundID")) %>
                            </ItemTemplate>
                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="new" HeaderText="" Visible="false" >
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openWinAdd('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                 DataBinder.Eval(Container.DataItem, "SoundID"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "SoundID"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SoundID" HeaderText="Sound ID"
                            UniqueName="SoundID" SortExpression="SoundID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Name" HeaderText="File Name" 
                            UniqueName="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Number" HeaderText="Number"
                            UniqueName="Number" SortExpression="Number" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsServiceCounter" HeaderText="Service Counter"
                            UniqueName="IsServiceCounter" SortExpression="IsServiceCounter" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="900px" DataField="FilePath" HeaderText="File Path"
                            UniqueName="FilePath" SortExpression="FilePath" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="true" />
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pgPoli" runat="server" >
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                
                                        <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                    OnItemsRequested="cboServiceUnitID_ItemsRequested" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" AutoPostBack="True">
                                            <FooterTemplate>
                                                Note : Show max 15 result
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png" OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </cc:CollapsePanel>
                                
            <telerik:RadGrid ID="grdPoli" runat="server" OnNeedDataSource="grdPoli_NeedDataSource" AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="ServiceUnitID" >
                    <Columns>               
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"gotoViewUrl1('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", 
                                DataBinder.Eval(Container.DataItem, "ServiceUnitID")))%>      
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" /> 
                        </telerik:GridTemplateColumn>
                     
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                              DataBinder.Eval(Container.DataItem, "ServiceUnitID"))) %>   
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                     
                        <telerik:GridBoundColumn DataField="ServiceUnitID" HeaderText="Service Unit ID"  UniqueName="ServiceUnitID" SortExpression="ServiceUnitID">
                            <HeaderStyle HorizontalAlign="Left" Width="70" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                      
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name" UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                  
                        <telerik:GridBoundColumn DataField="QueueCode" HeaderText="Queueing Code" UniqueName="QueueCode" SortExpression="QueueCode">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="stdLocRegName" HeaderText="Registration Queueing Location" UniqueName="stdLocRegName" SortExpression="stdLocRegName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="stdLocPoliName" HeaderText="Service Unit Queueing Location" UniqueName="stdLocPoliName" SortExpression="stdLocPoliName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="SoundFilePath" HeaderText="Audio File" UniqueName="SoundFilePath" SortExpression="SoundFilePath">
                            <HeaderStyle HorizontalAlign="Left" Width="800" /> 
                            <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>       
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                <FilterMenu></FilterMenu>             
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
   </telerik:RadMultiPage>
</asp:Content>
