<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Allotments3._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="FilterContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <label for="companyDropDown">Company</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="companyDropDown" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="col">
                <label for="yearDropDown">Year</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="yearDropDown" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="col">
                <label for="monthDropDown">Month</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="monthDropDown" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ResultsUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate></ContentTemplate> 
    </asp:UpdatePanel>
</asp:Content>
