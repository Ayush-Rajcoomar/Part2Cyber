using System;
using System.Collections.Generic;
using WPFModernVerticalMenu.Pages;

namespace WPFModernVerticalMenu.Pages
{
    public class MaxHeap
    {
        private List<ServiceRequest> heap;

        public MaxHeap()
        {
            heap = new List<ServiceRequest>();
        }

        // Method to get the priority value
        private int GetPriorityValue(string priority)
        {
            if (priority == "High") return 3;
            else if (priority == "Medium") return 2;
            else if (priority == "Low") return 1;
            else return 0;
        }

        // Method to insert a new service request into the heap
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        // Insert a list of requests into the heap
        public void InsertAll(List<ServiceRequest> requests)
        {
            foreach (var request in requests)
            {
                Insert(request);
            }
        }

        // Method to extract all elements from the heap in descending order
        public List<ServiceRequest> ExtractAll()
        {
            List<ServiceRequest> sortedList = new List<ServiceRequest>();
            while (heap.Count > 0)
            {
                sortedList.Add(ExtractMax());
            }
            return sortedList;
        }

        // Method to extract the service request with the highest priority
        public ServiceRequest ExtractMax()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            var max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return max;
        }

        // Heapify up (bubble up)
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (GetPriorityValue(heap[index].Priority) <= GetPriorityValue(heap[parentIndex].Priority))
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        // Heapify down (bubble down)
        private void HeapifyDown(int index)
        {
            int leftChild, rightChild, largest;
            while (true)
            {
                leftChild = 2 * index + 1;
                rightChild = 2 * index + 2;
                largest = index;

                if (leftChild < heap.Count && GetPriorityValue(heap[leftChild].Priority) > GetPriorityValue(heap[largest].Priority))
                    largest = leftChild;

                if (rightChild < heap.Count && GetPriorityValue(heap[rightChild].Priority) > GetPriorityValue(heap[largest].Priority))
                    largest = rightChild;

                if (largest == index) break;

                Swap(index, largest);
                index = largest;
            }
        }

        // Swap elements
        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
