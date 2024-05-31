<template>
  <section>
    <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
      <DeptTree
        class="w-1/4 xl:w-1/5"
        title="根目录"
        @nodeAction="handNodeAction"
        @select="handleSelect"
      />
      <template v-for="(item, index) in treeDepth" :key="item.id">
        <DeptTree
          class="w-1/4 xl:w-1/5"
          :parentId="item.id"
          :title="item.name"
          :depth="index"
          @select="handleSelect"
        />
      </template>
    </PageWrapper>

  </section>
</template>
<script lang="ts">
import { defineComponent, reactive, ref } from 'vue';

import { PageWrapper } from '/@/components/Page';
import DeptTree from './DeptTree.vue';
import { useGo } from '/@/hooks/web/usePage';
import { defHttp } from '/@/utils/http/axios';
import { message } from 'ant-design-vue';

export default defineComponent({
  name: 'CategotyManage',
  components: { PageWrapper, DeptTree },
  setup() {
    const treeDepth = ref<object[]>([]);

  
    const go = useGo();
    const searchInfo = reactive<Recordable>({});

  

    function handleSelect(nodeData: object, depth: number) {
      if (depth === undefined) {
        treeDepth.value.length = 0;
      } else {
        treeDepth.value.length = depth + 1;
      }
      if (nodeData) {
        treeDepth.value.push(nodeData);
      }
    }

    return {
      handleSelect,
      // handNodeAction,
      // handleOk,
      // modalTitle,
      searchInfo,
      treeDepth,
      // modalVisible,
      // formState,
    };
  },
});
</script>