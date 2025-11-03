<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="CompanyLaborProfileDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.CompanyLaborProfileDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblCompanyLaborProfileID" runat="server" Text="Company Labor Profile ID"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtCompanyLaborProfileID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCompanyLaborProfileCode" runat="server" Text="Company Labor Profile Code"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtCompanyLaborProfileCode" runat="server" Width="300px" MaxLength="10"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyLaborProfileCode" runat="server" ErrorMessage="Company Labor Profile Code required."
                    	ValidationGroup="entry" ControlToValidate="txtCompanyLaborProfileCode" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCompanyLaborProfileName" runat="server" Text="Company Labor Profile Name"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtCompanyLaborProfileName" runat="server" Width="300px" MaxLength="200"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyLaborProfileName" runat="server" ErrorMessage="Company Labor Profile Name required."
                    	ValidationGroup="entry" ControlToValidate="txtCompanyLaborProfileName" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
    
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Education" PageViewID="pgvEducation" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Field Of Work" PageViewID="pgvFieldOfWork">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    
    
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        
        <telerik:RadPageView ID="pgvEducation" runat="server">
            <telerik:RadGrid ID="grdCompanyEducationProfile" runat="server" OnNeedDataSource="grdCompanyEducationProfile_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdCompanyEducationProfile_UpdateCommand"
                OnDeleteCommand="grdCompanyEducationProfile_DeleteCommand" OnInsertCommand="grdCompanyEducationProfile_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="CompanyEducationProfileID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="CompanyEducationProfileID" HeaderText="Education Profile ID" UniqueName="CompanyEducationProfileID" SortExpression="CompanyEducationProfileID" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CompanyEducationProfileCode" HeaderText="Education Profile Code" UniqueName="CompanyEducationProfileCode" SortExpression="CompanyEducationProfileCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="CompanyEducationProfileName" HeaderText="Education Profile Name" UniqueName="CompanyEducationProfileName" SortExpression="CompanyEducationProfileName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime" HeaderText="Last Update Date Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID" HeaderText="Last Update By User ID" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
			        </Columns>
                    <EditFormSettings UserControlName="CompanyEducationProfileDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CompanyEducationProfileEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>

        </telerik:RadPageView>
        
        <telerik:RadPageView ID="pgvFieldOfWork" runat="server">
            <telerik:RadGrid ID="grdCompanyFieldOfWorkProfile" runat="server" OnNeedDataSource="grdCompanyFieldOfWorkProfile_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdCompanyFieldOfWorkProfile_UpdateCommand"
                OnDeleteCommand="grdCompanyFieldOfWorkProfile_DeleteCommand" OnInsertCommand="grdCompanyFieldOfWorkProfile_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="CompanyFieldOfWorkProfileID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="CompanyFieldOfWorkProfileID" HeaderText="Company Field Of Work Profile ID" UniqueName="CompanyFieldOfWorkProfileID" SortExpression="CompanyFieldOfWorkProfileID" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CompanyFieldOfWorkProfileCode" HeaderText="Company Field Of Work Profile Code" UniqueName="CompanyFieldOfWorkProfileCode" SortExpression="CompanyFieldOfWorkProfileCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="CompanyFieldOfWorkProfileName" HeaderText="Company Field Of Work Profile Name" UniqueName="CompanyFieldOfWorkProfileName" SortExpression="CompanyFieldOfWorkProfileName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime" HeaderText="Last Update Date Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID" HeaderText="Last Update By User ID" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
			        </Columns>
                    <EditFormSettings UserControlName="CompanyFieldOfWorkProfileDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CompanyFieldOfWorkProfileEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>

        </telerik:RadPageView>
        
    </telerik:RadMultiPage>
</asp:Content>

