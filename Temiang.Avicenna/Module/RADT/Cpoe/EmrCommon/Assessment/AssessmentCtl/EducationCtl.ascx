<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EducationCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EducationCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend>EDUCATION</legend>
    <table style="width: 100%">
        <tr>
            <td class="label">Initial Education To</td>
            <td style="width: 250px">
                <asp:RadioButtonList ID="optIsEducationToPatient" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Patient" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Patient Family, Name:" Value="0"></asp:ListItem>
                </asp:RadioButtonList>

            </td>
            <td>
                <telerik:RadTextBox ID="txtEducationRecipient" runat="server" Width="200px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td valign="top" class="label">Education</td>
            <td colspan="3">

                <telerik:RadGrid ID="grdEducation" Width="100%" Height="200px" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    GridLines="None" AllowMultiRowSelection="True" Skin=""
                    OnNeedDataSource="grdEducation_NeedDataSource" OnItemDataBound="grdEducation_ItemDataBound">
                    <MasterTableView DataKeyNames="ItemID" ShowHeader="False">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1" HeaderStyle-Width="30px">
                            </telerik:GridClientSelectColumn>
                            <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Education" HeaderStyle-Width="250px" />
                            <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes" />
                            <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit">
                                <ItemTemplate>
                                    <telerik:RadTextBox
                                        ID="txtNotes" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Notes") %>'
                                        Width="100%">
                                    </telerik:RadTextBox>

                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                        <Resizing AllowColumnResize="False" />
                        <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True"></Selecting>
                        <Scrolling UseStaticHeaders="True" AllowScroll="True"></Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>

            </td>
        </tr>

    </table>
</fieldset>
