<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ParticipantList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.ParticipantList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ParticipantID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ParticipantName" HeaderText="Participant Name"
                    UniqueName="ParticipantName" SortExpression="ParticipantName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear"
                    SortExpression="PeriodYear" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="QuarterPeriod" HeaderText="Quarter"
                    UniqueName="QuarterPeriod" SortExpression="QuarterPeriod" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="AppraisalType" HeaderText="Appraisal Type"
                    UniqueName="AppraisalType" SortExpression="AppraisalType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsScoringRecapitulation" HeaderText="Scoring Recapitulation"
                    UniqueName="IsScoringRecapitulation" SortExpression="IsScoringRecapitulation" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
