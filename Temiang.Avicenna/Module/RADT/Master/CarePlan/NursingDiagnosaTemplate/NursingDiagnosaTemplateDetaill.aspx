<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="NursingDiagnosaTemplateDetaill.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingDiagnosaTemplateDetaill" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%" valign="top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTemplateName" runat="server" Text="Template Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:HiddenField ID="hfTemplateID" runat="server" />
                            <telerik:RadTextBox ID="txtTemplateName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTemplateName" runat="server" ErrorMessage="Template name required."
                                ValidationGroup="entry" ControlToValidate="txtTemplateName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTemplateText" runat="server" Text="Template Text"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTemplateText" runat="server" Width="300px" MaxLength="300" TextMode="MultiLine" Height="130" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="grdRespondTemplateDetail" runat="server" OnNeedDataSource="grdRespondTemplateDetail_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None"
                    OnDeleteCommand="grdRespondTemplateDetail_DeleteCommand" 
                    OnInsertCommand="grdRespondTemplateDetail_InsertCommand"
                    OnUpdateCommand="grdRespondTemplateDetail_UpdateCommand" >
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="QuestionID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionID" HeaderText="Question ID"
                                UniqueName="QuestionID" SortExpression="QuestionID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="QuestionText" HeaderText="Question Text" UniqueName="QuestionText"
                                SortExpression="QuestionText" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                            <telerik:GridNumericColumn DataField="RowIndex" HeaderText="Row Index" UniqueName="RowIndex"
                                SortExpression="RowIndex" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="NursingDiagnosaTemplateDetailDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="NursingDiagnosaTemplateDetailEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td style="width: 50%" valign="top">
                <telerik:RadGrid ID="gridServiceUnit" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridServiceUnit_NeedDataSource" 
                    OnItemDataBound="gridServiceUnit_ItemDataBound">
                    <MasterTableView ClientDataKeyNames="ServiceUnitID" DataKeyNames="ServiceUnitID">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" /><br />
                                    <span>&nbsp;</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ServiceUnitID" HeaderText="Service Unit ID"
                                SortExpression="ServiceUnitID" UniqueName="ServiceUnitID">
                                <HeaderStyle HorizontalAlign="Left" Width="100%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name"
                                SortExpression="ServiceUnitName" UniqueName="ServiceUnitName">
                                <HeaderStyle HorizontalAlign="Left" Width="100%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
