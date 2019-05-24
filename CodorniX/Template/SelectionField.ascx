<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectionField.ascx.cs" Inherits="CodorniX.Template.SelectionField" %>
<asp:PlaceHolder ID="mainContainer" runat="server">
    <div class="row" style="padding-top: 10px;">
        <div class="col-md-2">
            <asp:Label ID="label" CssClass="form-control" runat="server" Text="Supervisor" />
        </div>
        <div class="col-md-9">
            <asp:DropDownList ID="dropdown" CssClass="form-control" OnSelectedIndexChanged="dropdown_SelectedIndexChanged" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-md-1">
            <asp:LinkButton ID="add" CssClass="btn btn-default btn-sm" OnClick="add_Click" runat="server">
                <span class="glyphicon glyphicon-plus"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="remove" CssClass="btn btn-default btn-sm disabled hidden" OnClick="remove_Click" runat="server">
                <span class="glyphicon glyphicon-minus"></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:PlaceHolder>
