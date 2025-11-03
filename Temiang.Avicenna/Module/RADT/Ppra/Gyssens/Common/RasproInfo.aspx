<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RasproInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Ppra.Common.RasproInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <table width="100%">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <table width="100%">

                        <tr>
                            <td class="label">RASPRO Type</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtRasproType" Width="100%" ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Infection Focus with Indication</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtAbRestriction" Width="100%" ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Diagnose</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtDiagnose" Width="100%" ReadOnly="true" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
    </fieldset>


    <telerik:RadGrid ID="grdRaspro" runat="server" OnNeedDataSource="grdRaspro_NeedDataSource" OnItemDataBound="grdRaspro_ItemDataBound"
        GridLines="None" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="RasproLineID">
            <Columns>
                <telerik:GridCheckBoxColumn DataField="IsEntryVisible" HeaderText="" UniqueName="IsEntryVisible" Visible="false">
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="SeqNo" HeaderText="No" UniqueName="SeqNo">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Spesification" HeaderText="Observation" UniqueName="Spesification">
                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridTemplateColumn UniqueName="Flow" HeaderText="">
                    <HeaderStyle Width="140px" />
                    <HeaderTemplate>
                        <table width="130px">
                            <tr>
                                <td style="width: 60px; align-content: center">Flow
                                </td>
                                <td style="align-content: center">Category
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="130px">
                            <tr>
                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes")? string.Empty:
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "YesAction"))? "style=\"width: 60px;background-color: #4CAF50;color:white;\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "YesAction").ToString())? "style=\"width: 60px;background-color: red;color:white;\"" :
                                            "style=\"width: 60px;background-color: #4CAF50;color:white;\""%>>
                                    <input type="radio" id="yes"
                                        name="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>"
                                        value="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_yes"
                                        <%# false.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible"))? "disabled":string.Empty %>
                                        <%#DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes")?"checked":string.Empty%>
                                        <%# true.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible")) 
                                                && !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes") ? "onclick=\"onItemFlowClick('"+DataBinder.Eval(Container.DataItem, "RasproLineID")+"_yes')\"" : string.Empty %> />
                                    <label for="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_yes">Ya</label></td>

                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes")? "style=\"text-align:center\"":
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "YesAction"))? "style=\"background-color: #4CAF50;color:white;font-weight:bold;text-align:center\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "YesAction").ToString())? "style=\"background-color: red;color:white;font-weight:bold;text-align:center\"" :
                                            "style=\"background-color: #4CAF50;color:white; text-align: center;\""%>>
                                    <%#DataBinder.Eval(Container.DataItem, "YesAction")%>
                                </td>
                            </tr>
                            <tr>
                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")? string.Empty:
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "NoAction"))? "style=\"width: 60px;background-color: #4CAF50;color:white;\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "NoAction").ToString())? "style=\"width: 60px;background-color: red;color:white;\"" :
                                            "style=\"width: 60px;background-color: #4CAF50;color:white;\""%>>
                                    <input type="radio" id="no"
                                        name="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>"
                                        value="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_no"
                                        <%# false.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible"))? "disabled":string.Empty %>
                                        <%#DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")?"checked":string.Empty%>
                                        <%# true.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible")) && !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")? "onclick=\"onItemFlowClick('"+DataBinder.Eval(Container.DataItem, "RasproLineID")+"_no')\"" : string.Empty%> />
                                    <label for="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_no">Tidak</label></td>

                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")? "style=\"text-align:center\"":
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "NoAction"))? "style=\"background-color: #4CAF50;color:white;font-weight:bold;text-align:center\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "NoAction").ToString())? "style=\"background-color: red;color:white;font-weight:bold;text-align:center\"" :
                                            "style=\"background-color: #4CAF50;color:white; text-align: center;\""%>>
                                    <%#DataBinder.Eval(Container.DataItem, "NoAction")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Action" HeaderText="Parameter Information">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "ParameterInfo")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>


    <asp:Literal runat="server" ID="litAbSuggestion"></asp:Literal>
</asp:Content>
