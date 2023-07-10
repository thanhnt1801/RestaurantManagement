import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TreeNode } from 'primeng/api';
import { Category } from 'src/app/_models/category';
import { Restaurant } from 'src/app/_models/restaurant';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
})
export class CategoryComponent {
  selectedNode?: TreeNode;
  orgChart: TreeNode[] = [];
  parents: Category[] = [];
  isDeleted: boolean = false;
  categories: Category[] = [];
  selectedRestaurant!: Restaurant | null;
  displayCategories: Category[] = [];
  categoryToEdit?: Category;
  loading: boolean = true;
  columnsToDisplay = [
    'parent',
    'name',
    'description',
    'created',
    'updated',
    'deleted',
    'edit-btn',
    'delete-btn',
  ];
  constructor(
    private _accountService: AccountService,
    public dialog: MatDialog,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this._accountService.currentRestaurant$.subscribe((restaurant) => {
      this.selectedRestaurant = restaurant;
    });
    if (!this.selectedRestaurant || this.selectedRestaurant.id === 0) {
      this.router.navigate(['/']);
    }
    this.loadCategories();
  }

  loadCategories(): void {
    this.loading = true;
    this._accountService.getAllCategory().subscribe((data) => {
      this.categories = data;
      this.displayCategories = this.categories.filter(
        (category) => category.restaurantId === this.selectedRestaurant?.id
      );
      this.parents = this.displayCategories;
      this.loading = false;
      const rootCategories = this.displayCategories.filter(
        (cate) => !cate.parent?.id
      );
      this.orgChart = [];
      if (rootCategories) {
        const children: TreeNode[] = [];
        rootCategories.forEach((category) => {
          const node: TreeNode = this.generateOrgChart(category);
          children.push(node);
        });
        this.orgChart.push({
          type: 'category',
          styleClass: 'p-category',
          expanded: true,
          data: {
            name: this.selectedRestaurant?.name,
            deleted: false,
            isRoot: true,
          },
          children: children,
        });
      }
      console.log('Org chart: ', this.orgChart);
    });
  }

  public generateOrgChart(category: any): TreeNode {
    const node: TreeNode = {
      type: 'category',
      styleClass: 'p-category',
      expanded: true,
      data: category,
      children: [],
    };
    const cate = this.displayCategories.find((c) => c.id === category.id);
    if (cate && cate.children) {
      const childrenData: Category[] = cate.children;
      const children: TreeNode[] = [];
      if (childrenData && Array.isArray(childrenData)) {
        childrenData.forEach((cate) => {
          const childrenNode: TreeNode = this.generateOrgChart(cate);
          children.push(childrenNode);
        });
      }
      node.children = children;
    }

    return node;
  }
  onNodeSelect(event: any) {
    console.log(' event node:', event.node.label);
  }

  onDelete(Id: number) {
    this._accountService.DeleteCategory(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, true);
        this.toastr.success('Delete successful');
      },
      error: (error) => console.log(error),
    });
  }

  onRestore(Id: number) {
    this._accountService.RestoreCategory(Id).subscribe({
      next: () => {
        this.updateDeletedValue(Id, false);
        this.toastr.success('Restore successful');
      },
      error: (error) => console.log(error),
    });
  }

  updateDeletedValue(Id: number, status: boolean) {
    const categoryToDelete = this.orgChart.find(
      (category) => category.data.id === Id
    );
    if (categoryToDelete) {
      categoryToDelete.data.deleted = status;
      this.orgChart = [...this.orgChart];
    }
  }
}
//dung `restaurant` thay vi (restaurant), () la khia bao danh sach, co the (restaurant, ...)
