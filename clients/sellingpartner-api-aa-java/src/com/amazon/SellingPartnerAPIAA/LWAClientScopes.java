package com.amazon.SellingPartnerAPIAA;


import com.google.gson.annotations.JsonAdapter;
import lombok.AllArgsConstructor;
import lombok.EqualsAndHashCode;
import lombok.Getter;

import java.util.Set;

@AllArgsConstructor
@Getter
@JsonAdapter(LWAClientScopesSerializerDeserializer.class)
@EqualsAndHashCode
public class LWAClientScopes {

    private final Set<String> scopes;

    protected void addScope(String scope) {
        scopes.add(scope);

    }

    protected boolean isEmpty() {
        return scopes.isEmpty();
    }


}
