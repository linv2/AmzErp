import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';

const dashboard: AppRouteModule = {
  path: '/prod',
  name: 'Product',
  component: LAYOUT,
  redirect: '/prod/list',
  meta: {
    orderNo: 10,
    icon: 'ion:grid-outline',
    title:"产品管理",
  },
  children: [
    {
      path: 'category',
      name: 'category',
      component: () => import('/@/views/product/category/index.vue'),
      meta: {
        // affix: true,
        title: "分类管理",
      },
    }
  ],
};

export default dashboard;
