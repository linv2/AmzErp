<template>
  <div class="bg-white overflow-hidden">
    <BasicTree
      :title="title"
      toolbar
      :treeData="treeData"
      :beforeRightClick="beforeRightClick"
      :replaceFields="{ key: 'id', title: 'title' }"
      @select="handleSelect"
    />

    <a-modal v-model:visible="modalVisible" :title="modalTitle" @ok="handleOk">
      <a-form
        :model="formState"
        :labelCol="{ span: 4 }"
        :wrapperCol="{ span: 13 }"
        style="margin-top: 20px"
      >
        <a-form-item label="节点名称">
          <a-input v-model:value="formState.name" />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>
<script lang="ts">
import { defineComponent, onMounted, reactive, ref } from 'vue';
import { defHttp } from '/@/utils/http/axios';

import { BasicTree, ContextMenuItem, TreeItem } from '/@/components/Tree';
import { any } from 'vue-types';
import linq from '/@/utils/linq';
import { ActionItem } from '/@/components/Table';
import { message, Modal } from 'ant-design-vue';
//import { getDeptList } from '/@/api/demo/system';

export default defineComponent({
  name: 'DeptTree',
  components: { BasicTree },
  props: {
    parentId: Number,
    depth: Number,
    title: String,
  },
  emits: ['select', 'nodeAction'],
  setup(props: any, { emit }) {
    const modalVisible = ref<Boolean>(false);
    const modalTitle = ref<string>();
    const formState = reactive({
      name: '',
      parentId: 0,
      id: 0,
    });
    const treeData = ref<TreeItem[]>([]);
    async function fetch(params) {
      const response = await defHttp.get({ url: '/erp/category/list', params });
      response.forEach((element) => {
        element.title = `${element.name}【${element.childCount}】`;
        if (!element.isEndable) {
          element.selectable = false;
          element.title = `${element.title}【已禁用】`;
        }
      });
      treeData.value = response;
    }

    function handleSelect(keys, e) {
      if (keys.length > 0) {
        emit('select', e.selectedNodes[0].props, props.depth);
      } else {
        emit('select', null, props.depth);
      }
    }

    const rightMenuList: ContextMenuItem[] = [];
    let rightMenuSelectObject: any = undefined;
    const nodeClick = (action) => {
      handNodeAction({ action, data: rightMenuSelectObject });
    };

    rightMenuList.push({
      label: '添加子节点',
      icon: 'ant-design:folder-add-outlined',
      handler() {
        nodeClick('add');
      },
    });
    rightMenuList.push({
      label: '添加节点',
      icon: 'ant-design:home-outlined',
      handler() {
        nodeClick('child');
      },
    });
    rightMenuList.push({
      label: '编辑节点',
      icon: 'ant-design:edit-filled',
      handler() {
        nodeClick('edit');
      },
    });
    rightMenuList.push({
      label: '删除节点',
      icon: 'ant-design:delete-filled',
      handler() {
        Modal.confirm({
          title: () => '提示?',
          content: () => '确认要删除该节点吗（删除后无法恢复）？',
          async onOk() {
            const response = await defHttp.get({
              url: '/erp/category/delete',
              params: { id: rightMenuSelectObject.id },
            });
            message.success(response);
            fetch({ parentId: props.parentId || 0 });
            return true;
          },
          // eslint-disable-next-line @typescript-eslint/no-empty-function
          onCancel() {},
        });
      },
    });
    rightMenuList.push({
      label: '禁用/启用节点',
      icon: 'ant-design:eye-invisible-filled',
      handler() {
        Modal.confirm({
          title: () => '提示?',
          content: () => '确认要禁用/启用该节点吗？',
          async onOk() {
            const response = await defHttp.get({
              url: '/erp/category/disable',
              params: { id: rightMenuSelectObject.id },
            });
            message.success(response);
            fetch({ parentId: props.parentId || 0 });
            return true;
          },
          // eslint-disable-next-line @typescript-eslint/no-empty-function
          onCancel() {},
        });
      },
    });

    let url = '';
    async function handleOk() {
      const response = defHttp.post({ url, data: formState });
      message.success(response);
    }
    function handNodeAction(param) {
      switch (param.action) {
        case 'add':
          {
            url = '/erp/category/add';
            formState.parentId = param.data.parentId;
            formState.id = 0;
            formState.name = '';
            modalTitle.value = '添加节点';
          }
          break;
        case 'child':
          {
            url = '/erp/category/add';
            formState.parentId = param.data.id;
            formState.id = 0;
            formState.name = '';
            modalTitle.value = '添加子节点';
          }
          break;
        case 'edit':
          {
            url = '/erp/category/edit';
            formState.parentId = 0;
            formState.id = param.data.id;
            formState.name = param.data.name;
            modalTitle.value = '编辑节点';
          }
          break;
      }
      modalVisible.value = true;
    }

    function beforeRightClick(node, event) {
      rightMenuSelectObject = linq
        .from(treeData.value)
        .firstOrDefault((x) => x.id == node.eventKey);
      return rightMenuList;
    }
    onMounted(() => {
      fetch({ parentId: props.parentId || 0 });
    });
    return {
      treeData,
      handleSelect,
      beforeRightClick,
      modalVisible,
      modalTitle,
      formState,
      handleOk,
    };
  },
});
</script>

<style scoped>
span.ant-dropdown-trigger {
  display: none;
}
</style>