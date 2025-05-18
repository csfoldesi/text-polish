import { client, useAuthClient } from "@/client";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { AxiosError } from "axios";

type Request = {
  text: string;
};

export const useTest = () => {
  const { setClientToken } = useAuthClient();
  const queryClient = useQueryClient();

  const mutation = useMutation<string, AxiosError, Request>({
    mutationFn: async (data: Request) => {
      await setClientToken(client);
      return client.post(`/text`, data).then((res) => res.data);
    },
    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({ queryKey: ["Text", variables.text] });
    },
  });

  return { runTest: mutation.mutateAsync, isPending: mutation.isPending };
};
